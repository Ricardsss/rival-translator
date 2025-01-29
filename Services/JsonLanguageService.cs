using System.Text.Json;
using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;

public class JsonLanguageService : ILanguageService
{
    private readonly IWebHostEnvironment _env;
    private readonly string _filePath;
    private readonly IConfiguration _configuration;
    private readonly ILoggerService _logger;


    public JsonLanguageService(IWebHostEnvironment env, IConfiguration configuration, ILoggerService logger)
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        string fileFolder = _configuration["JsonData:Folder"] ?? "resources";
        string fileName = _configuration["JsonData:File"] ?? "languages.json";
        _filePath = Path.Combine(_env.ContentRootPath, fileFolder, fileName);
    }

    public Dictionary<string, string> GetLanguages()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException($"Language file not found at {_filePath}.");
            }

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