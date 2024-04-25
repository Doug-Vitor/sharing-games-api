using System.Text.Json;

public static class JsonExtensions
{
  public static T FromJson<T>(this string json)
  {
    JsonSerializerOptions options = new()
    {
      PropertyNameCaseInsensitive = true
    };

    return JsonSerializer.Deserialize<T>(json, options) ?? throw new FormatException();
  }

  public static string ToJson<T>(this T serializable) => JsonSerializer.Serialize<T>(serializable);
}