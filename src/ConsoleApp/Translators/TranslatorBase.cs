using System.Resources;

namespace MultilingualExploration.Translators;

public abstract class TranslatorBase(string translatorName) : ITranslator
{

	protected ResourceManager rm = new("MultilingualExploration.Localization.Resources", typeof(Program).Assembly);

	protected abstract Task<GetLocalizedStringResponse> GetLocalizedStringAsync(CultureInfo cultureInfo, string key, string defaultText);

	public int CharactersConverted { get; private set; }

	public string TranslatorName { get; } = translatorName;

	public async Task<string> GetGreetingAsync(CultureInfo cultureInfo)
	{
		GetLocalizedStringResponse language = await GetLocalizedStringAsync(cultureInfo, "Language", GetDefaultText("Language"));
		GetLocalizedStringResponse culture = await GetLocalizedStringAsync(cultureInfo, "Culture", GetDefaultText("Culture"));
		GetLocalizedStringResponse greeting = await GetLocalizedStringAsync(cultureInfo, "Greeting", GetDefaultText("Greeting"));
		GetLocalizedStringResponse greetings = await GetLocalizedStringAsync(cultureInfo, "Greetings", GetGreetingInEnglish(cultureInfo));
		CharactersConverted += language.CharactersConverted + culture.CharactersConverted + greeting.CharactersConverted + greetings.CharactersConverted;
		return $"[invert]{cultureInfo.DisplayName}[/]\n[b]{language.TranslatedText}[/]: {await GetLanguageAsync(cultureInfo)}\n[b]{culture.TranslatedText}[/]: {cultureInfo.DisplayName}\n[b]{greeting.TranslatedText}[/]: {greetings.TranslatedText}";
	}

	private async Task<string> GetLanguageAsync(CultureInfo cultureInfo)
	{
		GetLocalizedStringResponse getLocalizedStringResponse = cultureInfo.Name switch
		{
			"en-US" => await GetLocalizedStringAsync(cultureInfo, "English", cultureInfo.NativeName),
			"cs-CZ" => await GetLocalizedStringAsync(cultureInfo, "Czech", cultureInfo.NativeName),
			"fr-FR" => await GetLocalizedStringAsync(cultureInfo, "French", cultureInfo.NativeName),
			"de-DE" => await GetLocalizedStringAsync(cultureInfo, "German", cultureInfo.NativeName),
			"es-MX" or "es-PR" => await GetLocalizedStringAsync(cultureInfo, "Spanish", cultureInfo.NativeName),
			"fil-PH" => await GetLocalizedStringAsync(cultureInfo, "Filipino", cultureInfo.NativeName),
			_ => new GetLocalizedStringResponse
			{
				OriginalText = cultureInfo.NativeName,
				TranslatedText = "Unknown",
				CharactersConverted = 0
			},
		};
		CharactersConverted += getLocalizedStringResponse.CharactersConverted;
		return getLocalizedStringResponse.TranslatedText;
	}

	private string GetDefaultText(string key)
		=> rm.GetString(key, new CultureInfo("en-US")) ?? rm.GetString(key, CultureInfo.InvariantCulture) ?? key;

	private string GetGreetingInEnglish(CultureInfo cultureInfo)
	{
		if (cultureInfo.Name == "cs-CZ")
			return GetDefaultText("HelloCzechRepublic");
		else if (cultureInfo.Name == "de-DE")
			return GetDefaultText("HelloGermany");
		else if (cultureInfo.Name == "es-MX")
			return GetDefaultText("HelloMexico");
		else if (cultureInfo.Name == "es-PR")
			return GetDefaultText("HelloPuertoRico");
		else if (cultureInfo.Name == "fil-PH")
			return GetDefaultText("HelloPhilippines");
		else if (cultureInfo.Name == "fr-FR")
			return GetDefaultText("HelloFrance");
		else
			return GetDefaultText("Greetings");
	}

}