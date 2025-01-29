using System.Text.Json;
using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;

public class JsonLanguageProvider : Service, ILanguageProvider
{
    private readonly IWebHostEnvironment _env;
    private readonly string _filePath;
    private readonly IConfiguration _configuration;


    public JsonLanguageProvider(IWebHostEnvironment env, IConfiguration configuration)
    {
        _env = env ?? throw new ArgumentNullException(nameof(env));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        string fileFolder = _configuration["JsonData:Folder"] ?? "resources";
        string fileName = _configuration["JsonData:File"] ?? "languages.json";
        _filePath = Path.Combine(_env.ContentRootPath, fileFolder, fileName);
    }

    public Dictionary<string, string> GetLanguages()
    {
        return HandleExceptions(() =>
        {
            if (!File.Exists(_filePath))
            {
                throw new FileNotFoundException($"Language file not found at {_filePath}.");
            }
            string jsonContent = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonContent)
                   ?? throw new InvalidOperationException("Failed to deserialize JSON.");
        }, "Failed to retrieve languages.");
    }
}