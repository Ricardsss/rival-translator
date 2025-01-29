using Google.Cloud.Translation.V2;
using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;



public class TranslationService : Service, ITranslationService
{
  private readonly TranslationClient _translationClient;

  public TranslationService(IConfiguration configuration)
  {
    if (configuration == null) throw new ArgumentNullException(nameof(configuration));

    string apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY") ?? string.Empty;

    if (string.IsNullOrEmpty(apiKey))
    {
      throw new InvalidOperationException("API key for Google Cloud Translation is missing.");
    }
    _translationClient = TranslationClient.CreateFromApiKey(apiKey);
  }

  public string TranslateText(string text, string targetLanguage, string sourceLanguage)
  {
    return HandleExceptions(() =>
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        throw new ArgumentException("Text to translate cannot be null or empty.", nameof(text));
      }

      if (string.IsNullOrEmpty(targetLanguage) || string.IsNullOrEmpty(sourceLanguage))
      {
        throw new ArgumentException("Both target and source languages must be specified.");
      }

      var response = _translationClient.TranslateText(text, targetLanguage, sourceLanguage);
      return response.TranslatedText;
    }, "Failed to translate text.");
  }
}