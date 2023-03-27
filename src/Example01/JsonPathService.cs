using Newtonsoft.Json.Linq;

namespace Example01;

public interface IJsonPathService
{
    ICollection<string> GetJsonPaths(string json, string propertyName);
}

public class JsonPathService : IJsonPathService
{
    public ICollection<string> GetJsonPaths(string json, string propertyName)
    {
        var paths = JObject.Parse(json)
            .Descendants()
            .OfType<JProperty>()
            .Where(x => HasPropertyName(x, propertyName))
            .Select(x => $"$.{x.Path}")
            .ToList();
        return paths;
    }

    private static bool HasPropertyName(JProperty property, string propertyName)
    {
        return string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase);
    }
}