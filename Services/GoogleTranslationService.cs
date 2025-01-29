using Google.Cloud.Translation.V2;
using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;



public class GoogleTranslationService : ITranslationService, ILanguageDetectionService
{
  private readonly TranslationClient _translationClient;
  private readonly ILoggerService _logger;

  public GoogleTranslationService(IConfiguration configuration, ILoggerService logger)
  {
    if (configuration == null) throw new ArgumentNullException(nameof(configuration));

    string apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY") ?? string.Empty;
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    if (string.IsNullOrEmpty(apiKey))
    {
      throw new InvalidOperationException("API key for Google Cloud Translation is missing.");
    }
    _translationClient = TranslationClient.CreateFromApiKey(apiKey);
  }

  public string TranslateText(string text, string targetLanguage, string sourceLanguage)
  {
    try
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
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to translate text: {ex.Message}");
      throw new InvalidOperationException("Failed to translate text.", ex);
    }
  }
  public string DetectLanguage(string text)
  {
    try
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        throw new ArgumentException("Text cannot be null or empty.", nameof(text));
      }

      var response = _translationClient.DetectLanguage(text);
      return response.Language;
    }
    catch (Exception ex)
    {
      _logger.LogError($"Failed to detect language: {ex.Message}");
      throw new InvalidOperationException("Failed to detect language.", ex);
    }
  }
}