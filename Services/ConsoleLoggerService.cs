using RivalTranslator.Interfaces;

namespace RivalTranslator.Services;

public class ConsoleLoggerService : ILoggerService
{
  public void Log(string message)
  {
    Console.WriteLine($"[{DateTime.Now}] {message}");
  }

  public void LogError(string message)
  {
    Console.Error.WriteLine($"[{DateTime.Now}] ERROR: {message}");
  }
}
