namespace RivalTranslator.Interfaces;

public interface ITranslationService
{
  string TranslateText(string text, string targetLanguage, string sourceLanguage);
}
