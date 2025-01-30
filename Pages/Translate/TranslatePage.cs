using Microsoft.AspNetCore.Mvc.RazorPages;
using RivalTranslator.Interfaces;

namespace RivalTranslator.Pages.Translate;

public abstract class TranslatePage : PageModel
{
  protected readonly ITranslationService GoogleTranslationService;
  protected readonly IConfiguration Configuration;
  private readonly ILanguageProviderService _languageProvider;
  public Dictionary<string, string> LanguageMap { get; private set; } = new();

  protected TranslatePage(ITranslationService translationService, ILanguageProviderFactory languageProviderFactory, IConfiguration configuration)
  {
    GoogleTranslationService = translationService ?? throw new ArgumentNullException(nameof(translationService));
    _languageProvider = _languageProvider = languageProviderFactory.GetLanguageProvider();
    Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    LoadLanguageMap();
  }

  private void LoadLanguageMap()
  {
    try
    {
      LanguageMap = _languageProvider.GetLanguages() ?? new Dictionary<string, string>();
    }
    catch
    {
      LanguageMap = new Dictionary<string, string>();
    }
  }
}
