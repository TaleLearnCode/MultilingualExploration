namespace MultilingualExploration.Translators;

public class GetLocalizedStringResponse
{
	public string OriginalText { get; set; } = null!;
	public string TranslatedText { get; set; } = null!;
	public int CharactersConverted { get; set; }
}