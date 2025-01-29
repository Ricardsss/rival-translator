using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;

public class LanguageValidationService : Service, ILanguageValidationService
{
  public bool ValidateLanguages(string sourceLanguage, string targetLanguage, out string errorMessage)
  {
    if (string.IsNullOrWhiteSpace(sourceLanguage) || string.IsNullOrWhiteSpace(targetLanguage))
    {
      errorMessage = "Both source and target languages must be specified.";
      return false;
    }

    if (sourceLanguage == targetLanguage)
    {
      errorMessage = "Source and target languages cannot be the same.";
      return false;
    }

    errorMessage = string.Empty;
    return true;
  }
}
