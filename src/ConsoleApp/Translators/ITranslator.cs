using System.Globalization;

namespace MultilingualExploration.Translators;

public interface ITranslator
{
	int CharactersConverted { get; }
	string TranslatorName { get; }

	Task<string> GetGreetingAsync(CultureInfo cultureInfo);
}