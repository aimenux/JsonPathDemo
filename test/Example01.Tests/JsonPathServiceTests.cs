using FluentAssertions;

namespace Example01.Tests;

public class JsonPathServiceTests
{
    [Theory]
    [ClassData(typeof(TestCases))]
    public void Should_Get_Json_Paths_For_Property_Name(string propertyName, string[] expectedPaths)
    {
        // arrange
        const string json =
        """
        {
            "firstName": "John",
            "lastName": "doe",
            "age": 26,
            "address": 
            {
                "streetAddress": "15 street",
                "city": "Nara",
                "postalCode": "630-0192"
            },
            "phoneNumbers": 
            [
             {
                "type": "iPhone",
                "number": "0123-4567-8888"
             },
             {
                "type": "home",
                "number": "0123-4567-8910"
             }
            ]
        }
        """;

        var service = new JsonPathService();

        // act
        var paths = service.GetJsonPaths(json, propertyName);

        // assert
        paths.Should().NotBeNull().And.BeEquivalentTo(expectedPaths);
    }
    
    private class TestCases : TheoryData<string, string[]>
    {
        public TestCases()
        {
            Add("age", new[] { "$.age" });
            Add("xyz", Array.Empty<string>());
            Add("city", new[] { "$.address.city" });
            Add("firstName", new[] { "$.firstName" });
            Add("streetAddress", new[] { "$.address.streetAddress" });
            Add("type", new[] { "$.phoneNumbers[0].type", "$.phoneNumbers[1].type" });
            Add("number", new[] { "$.phoneNumbers[0].number", "$.phoneNumbers[1].number" });
        }
    }
}