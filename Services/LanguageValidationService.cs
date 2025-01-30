using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;

public class LanguageValidationService : ILanguageValidationService
{
  public bool ValidateLanguages(string sourceLanguage, string targetLanguage, out string errorMessage)
  {
    InputValidationService.ValidateNotNullOrEmpty(sourceLanguage, nameof(sourceLanguage));
    InputValidationService.ValidateNotNullOrEmpty(targetLanguage, nameof(targetLanguage));

    if (sourceLanguage == targetLanguage)
    {
      errorMessage = "Source and target languages cannot be the same.";
      return false;
    }

    errorMessage = string.Empty;
    return true;
  }
}
