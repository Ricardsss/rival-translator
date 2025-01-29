namespace RivalTranslator.Interfaces;

public interface ILanguageValidationService
{
  bool ValidateLanguages(string sourceLanguage, string targetLanguage, out string errorMessage);
}
