namespace RivalTranslator.Pages.Translate;

public class TranslateViewModel
{
  public Dictionary<string, string> LanguageMap { get; set; } = new();
  public TranslationState State { get; set; } = new();
}
