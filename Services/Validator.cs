namespace RivalTranslator.Services;

public static class Validator
{
  public static void ValidateNotNullOrEmpty(string input, string parameterName)
  {
    if (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
    {
      throw new ArgumentException($"{parameterName} cannot be null or empty.");
    }
  }

  public static void ValidateNotNull(object parameter, string parameterName)
  {
    if (parameter == null)
    {
      throw new ArgumentNullException($"{parameterName} cannot be null.");
    }
  }

  public static void ValidateFileFound(string filePath)
  {
    if (!File.Exists(filePath))
    {
      throw new FileNotFoundException($"File not found at {filePath}.");
    }
  }
}