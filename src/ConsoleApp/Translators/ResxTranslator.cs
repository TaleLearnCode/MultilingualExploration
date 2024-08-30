using System.Globalization;

namespace MultilingualExploration.Translators;

public class ResxTranslator : TranslatorBase, ITranslator
{

	public ResxTranslator() : base("Resource Files") { }

	protected override async Task<GetLocalizedStringResponse> GetLocalizedStringAsync(CultureInfo cultureInfo, string key, string defaultText)
		=> new GetLocalizedStringResponse
		{
			OriginalText = defaultText,
			TranslatedText = rm.GetString(key, cultureInfo) ?? defaultText,
			CharactersConverted = 0
		};

}