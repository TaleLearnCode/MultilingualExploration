using Amazon;
using Amazon.Translate;
using Amazon.Translate.Model;

namespace MultilingualExploration.Translators;

public class AWSTranslator : TranslatorBase, ITranslator
{
	public AWSTranslator() : base("AWS Translator") { }

	protected override async Task<GetLocalizedStringResponse> GetLocalizedStringAsync(System.Globalization.CultureInfo cultureInfo, string key, string defaultText)
	{

		string? translatedText = null;
		int charactersConverted = 0;

		if (cultureInfo.Name != "en-US")
		{
			charactersConverted = defaultText.Length;
			AmazonTranslateClient client = new(RegionEndpoint.USEast1);
			TranslateTextRequest translateTextRequest = new()
			{
				Text = defaultText,
				SourceLanguageCode = "en",
				TargetLanguageCode = GetLanguageCode(cultureInfo)
			};
			TranslateTextResponse response = await client.TranslateTextAsync(translateTextRequest);
			translatedText = response.TranslatedText;
		}

		return new GetLocalizedStringResponse
		{
			OriginalText = defaultText,
			TranslatedText = translatedText ?? defaultText,
			CharactersConverted = charactersConverted
		};

	}

	private string GetLanguageCode(CultureInfo cultureInfo)
	{
		return cultureInfo.Name switch
		{
			"fil-PH" => "tl",
			_ => cultureInfo.Name
		};
	}

}