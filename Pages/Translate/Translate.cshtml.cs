using Microsoft.AspNetCore.Mvc;
using RivalTranslator.Interfaces;

namespace RivalTranslator.Pages.Translate;

public class TranslateModel : TranslatePage
{
  public TranslateViewModel ViewModel { get; set; } = new();
  private readonly ILanguageValidationService _languageValidationService;

  public TranslateModel(ITranslationService translationService, ILanguageProviderService languageProvider, IConfiguration configuration, ILanguageValidationService languageValidationService)
      : base(translationService, languageProvider, configuration)
  {
    _languageValidationService = languageValidationService;
    ViewModel.LanguageMap = LanguageMap;
    ViewModel.State.SelectedSourceLanguage = Configuration["DefaultLanguages:Source"] ?? "English (UK)";
    ViewModel.State.SelectedTargetLanguage = Configuration["DefaultLanguages:Target"] ?? "French";
  }

  public void OnGet()
  {
    if (TempData.ContainsKey("TranslatedText"))
    {
      ViewModel.State.SelectedSourceLanguage = TempData["SelectedSourceLanguage"] as string ?? ViewModel.State.SelectedSourceLanguage;
      ViewModel.State.SelectedTargetLanguage = TempData["SelectedTargetLanguage"] as string ?? ViewModel.State.SelectedTargetLanguage;
      ViewModel.State.TranslatedText = TempData["TranslatedText"] as string ?? string.Empty;
      ViewModel.State.InputText = TempData["InputText"] as string ?? string.Empty;
    }
  }

  public IActionResult OnPost(string text, string selectedTargetLanguage, string selectedSourceLanguage)
  {
    ViewModel.State.SelectedSourceLanguage = selectedSourceLanguage;
    ViewModel.State.SelectedTargetLanguage = selectedTargetLanguage;
    ViewModel.State.InputText = text;

    if (!_languageValidationService.ValidateLanguages(selectedSourceLanguage, selectedTargetLanguage, out string validationMessage))
    {
      ViewModel.State.TranslatedText = validationMessage;
      TempData["TranslatedText"] = validationMessage;
      return RedirectToPage();
    }

    try
    {
      ViewModel.State.TranslatedText = GoogleTranslationService.TranslateText(ViewModel.State.InputText, ViewModel.LanguageMap[ViewModel.State.SelectedTargetLanguage], ViewModel.LanguageMap[ViewModel.State.SelectedSourceLanguage]);
    }
    catch (Exception ex)
    {
      ViewModel.State.TranslatedText = $"Translation failed: {ex.Message}";
    }

    TempData["TranslatedText"] = ViewModel.State.TranslatedText;
    TempData["SelectedSourceLanguage"] = ViewModel.State.SelectedSourceLanguage;
    TempData["SelectedTargetLanguage"] = ViewModel.State.SelectedTargetLanguage;
    TempData["InputText"] = ViewModel.State.InputText;

    return RedirectToPage();
  }
}