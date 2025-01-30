namespace RivalTranslator.Services;

public static class InputValidationService
{
  public static void ValidateNotNullOrEmpty(string input, string parameterName)
  {
    if (string.IsNullOrWhiteSpace(input) || string.IsNullOrEmpty(input))
    {
      throw new ArgumentException($"{parameterName} cannot be null or empty.");
    }
  }
}