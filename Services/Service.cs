namespace RivalTranslator.Services;

public abstract class Service
{
  protected void Log(string message)
  {
    Console.WriteLine($"[{DateTime.Now}] {message}");
  }

  protected void LogError(string message)
  {
    Console.Error.WriteLine($"[{DateTime.Now}] ERROR: {message}");
  }

  protected T HandleExceptions<T>(Func<T> operation, string errorMessage)
  {
    try
    {
      return operation();
    }
    catch (Exception ex)
    {
      LogError($"{errorMessage}: {ex.Message}");
      throw new InvalidOperationException(errorMessage, ex);
    }
  }
}