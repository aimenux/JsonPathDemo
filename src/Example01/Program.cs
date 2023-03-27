using Example01;

const string propertyName = @"qux";

const string json =
    """
    { 
        "foo": { 
            "bar": 1, 
            "baz": [
            { 
              "qux": "a" 
            },
            { 
              "qux": "b"
            }
          ] 
        }
    }
    """;

var service = new JsonPathService();
var paths = service.GetJsonPaths(json, propertyName);
Console.WriteLine($"Found {paths.Count} json path(s)");
foreach (var path in paths)
{
    Console.WriteLine($"- Path : {path}");
}