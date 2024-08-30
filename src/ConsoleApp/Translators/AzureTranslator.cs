using Azure;
using Azure.AI.Translation.Text;

namespace MultilingualExploration.Translators;

public class AzureTranslator : TranslatorBase, ITranslator
{

	private readonly AzureKeyCredential credential;
	private readonly TextTranslationClient client;

	public AzureTranslator(string key) : base("Azure Translator")
	{
		credential = new(key);
		client = new(credential);
	}

	protected override async Task<GetLocalizedStringResponse> GetLocalizedStringAsync(CultureInfo cultureInfo, string key, string defaultText)
	{
		string? translatedText = null;
		int charactersConverted = 0;

		// TODO: Put the following code in a try-catch block; handle the RequestFailedException; exception.ErrorCode and exception.Message
		if (cultureInfo.Name != "en-US")
		{
			Response<IReadOnlyList<TranslatedTextItem>> response = await client.TranslateAsync(cultureInfo.Name, defaultText).ConfigureAwait(false);
			IReadOnlyList<TranslatedTextItem> translations = response.Value;
			TranslatedTextItem? translation = translations.FirstOrDefault();
			translatedText = translation?.Translations?.FirstOrDefault()?.Text;
			charactersConverted = defaultText.Length;
		}

		//return translatedText ?? defaultText;
		return new GetLocalizedStringResponse
		{
			OriginalText = defaultText,
			TranslatedText = translatedText ?? defaultText,
			CharactersConverted = charactersConverted
		};
	}

}