using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;

public class LanguageProviderFactory : ILanguageProviderFactory
{
  private readonly ILanguageProviderService _jsonLanguageProvider;

  public LanguageProviderFactory(ILanguageProviderService jsonLanguageProvider)
  {
    _jsonLanguageProvider = jsonLanguageProvider;
  }

  public ILanguageProviderService GetLanguageProvider()
  {
    return _jsonLanguageProvider;
  }
}
