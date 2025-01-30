using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;

public class LanguageValidationService : ILanguageValidationService
{
  public bool ValidateLanguages(string sourceLanguage, string targetLanguage, out string errorMessage)
  {
    Validator.ValidateNotNullOrEmpty(sourceLanguage, nameof(sourceLanguage));
    Validator.ValidateNotNullOrEmpty(targetLanguage, nameof(targetLanguage));

    if (sourceLanguage == targetLanguage)
    {
      errorMessage = "Source and target languages cannot be the same.";
      return false;
    }

    errorMessage = string.Empty;
    return true;
  }
}
