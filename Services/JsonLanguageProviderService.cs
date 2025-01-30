using System.Text.Json;
using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;

public class JsonLanguageProviderService : ILanguageProviderService
{
    private readonly IWebHostEnvironment _env;
    private readonly string _filePath;
    private readonly IConfiguration _configuration;
    private readonly ILoggerService _logger;


    public JsonLanguageProviderService(IWebHostEnvironment env, IConfiguration configuration, ILoggerService logger)
    {
        Validator.ValidateNotNull(env, nameof(env));
        _env = env;

        Validator.ValidateNotNull(configuration, nameof(configuration));
        _configuration = configuration;

        Validator.ValidateNotNull(logger, nameof(logger));
        _logger = logger;

        string fileFolder = _configuration["JsonData:Folder"] ?? "resources";
        string fileName = _configuration["JsonData:File"] ?? "languages.json";
        _filePath = Path.Combine(_env.ContentRootPath, fileFolder, fileName);
    }

    public Dictionary<string, string> GetLanguages()
    {
        try
        {
            Validator.ValidateFileFound(_filePath);
            string jsonContent = File.ReadAllText(_filePath);

            return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent)
       ?? throw new InvalidOperationException("Failed to deserialize JSON.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to retrieve languages: {ex.Message}");
            throw new InvalidOperationException("Failed to retrieve languages.", ex);
        }
    }
}