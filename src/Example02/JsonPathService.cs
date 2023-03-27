﻿using System.Text.Json;

namespace Example02;

public interface IJsonPathService
{
    ICollection<string> GetJsonPaths(string json, string propertyName);
}

public class JsonPathService : IJsonPathService
{
    public ICollection<string> GetJsonPaths(string json, string propertyName)
    {
        using var document = JsonDocument.Parse(json);
        return GetJsonPaths(document.RootElement, propertyName);
    }
    
    private static ICollection<string> GetJsonPaths(JsonElement element, string propertyName, string path = "$")
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
            {
                var result = new List<string>();
                foreach (var property in element.EnumerateObject())
                {
                    var propertyPath = $"{path}.{property.Name}";
                    result.AddRange(GetJsonPaths(property.Value, propertyName, propertyPath));
                    if (HasPropertyName(property, propertyName))
                    {
                        result.Add(propertyPath);
                    }
                }
                return result;
            }
            case JsonValueKind.Array:
            {
                var result = new List<string>();
                for (var i = 0; i < element.GetArrayLength(); i++)
                {
                    result.AddRange(GetJsonPaths(element[i], propertyName, $"{path}[{i}]"));
                }
                return result;
            }
            default:
                return Array.Empty<string>();
        }
    }
    
    private static bool HasPropertyName(JsonProperty property, string propertyName)
    {
        return string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase);
    }
}