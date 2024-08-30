namespace MultilingualExploration;

internal class DemoKeys
{

	private readonly string _azureAITranslatorKey;

	internal DemoKeys()
	{
		_azureAITranslatorKey = GetEnvironmentVariable("AzureTranslatorKey");
	}

	internal string AzureAITranslator => _azureAITranslatorKey;

	private static string GetEnvironmentVariable(string key)
	{
		try
		{
			return Environment.GetEnvironmentVariable(key)!;
		}
		catch (Exception ex)
		{
			throw new Exception($"Error getting environment variable {key}.", ex);
		}
	}

}