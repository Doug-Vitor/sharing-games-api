using System.Text.Json;

public static class JsonExtensions
{
  static readonly JsonSerializerOptions options = new()
  {
    PropertyNameCaseInsensitive = true
  };

  public static T FromJson<T>(this string json)
    => JsonSerializer.Deserialize<T>(json, options) ?? throw new FormatException();

  public static string ToJson<T>(this T serializable) => JsonSerializer.Serialize(serializable, options);
}