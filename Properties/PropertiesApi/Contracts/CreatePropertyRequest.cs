using System.Text.Json.Serialization;

namespace PropertiesApi.Contracts;

public class CreatePropertyRequest
{
    [JsonPropertyName( "Name" )]
    public string Name { get; set; }
}
