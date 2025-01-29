namespace RivalTranslator.Interfaces;

public interface ILoggerService
{
  void Log(string message);
  void LogError(string message);
}
