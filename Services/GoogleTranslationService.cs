using Google.Cloud.Translation.V2;
using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;



public class GoogleTranslationService : ITranslationService, ILanguageDetectionService
{
  private readonly TranslationClient _translationClient;
  private readonly ILoggerService _logger;

  public GoogleTranslationService(IConfiguration configuration, ILoggerService logger)
  {
    Validator.ValidateNotNull(configuration, nameof(configuration));

    Validator.ValidateNotNull(logger, nameof(logger));
    _logger = logger;

    string apiKey = Environment.GetEnvironmentVariable("GOOGLE_API_KEY") ?? string.Empty;
    Validator.ValidateNotNullOrEmpty(apiKey, nameof(apiKey));

    _translationClient = TranslationClient.CreateFromApiKey(apiKey);
  }

  public string TranslateText(string text, string targetLanguage, string sourceLanguage)
  {
    try
    {
      Validator.ValidateNotNullOrEmpty(text, nameof(text));
      Validator.ValidateNotNullOrEmpty(targetLanguage, nameof(targetLanguage));
      Validator.ValidateNotNullOrEmpty(sourceLanguage, nameof(sourceLanguage));

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
      Validator.ValidateNotNullOrEmpty(text, nameof(text));

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