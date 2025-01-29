namespace RivalTranslator.Pages.Translate;

public class TranslationState
{
  public string SelectedSourceLanguage { get; set; } = string.Empty;
  public string SelectedTargetLanguage { get; set; } = string.Empty;
  public string InputText { get; set; } = string.Empty;
  public string TranslatedText { get; set; } = string.Empty;
}
