using Example01;

const string propertyName = @"city";

const string json = @"
{
    ""name"": ""John Doe"",
    ""age"": 42,
    ""address"": {
        ""street"": ""123 Main St"",
        ""city"": ""Anywhere"",
        ""state"": ""CA"",
        ""zip"": ""12345""
    }
}";

var service = new JsonPathService();
var paths = service.GetJsonPaths(json, propertyName);
Console.WriteLine($"Found {paths.Count} json path(s)");
foreach (var path in paths)
{
    Console.WriteLine($"- Path : {path}");
}