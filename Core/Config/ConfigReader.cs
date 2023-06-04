using System.Text.Json.Serialization;
using System.Text.Json;

namespace Core.Config;

public static class ConfigReader
{
    public static TestSettings ReadConfig()
    {
        var fullPath = Path.GetFullPath("appsettings.json", Paths.AssemblyPath);
        var configFile = File.ReadAllText(fullPath);

        var jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
        };

        jsonOptions.Converters.Add(new JsonStringEnumConverter());

        return JsonSerializer.Deserialize<TestSettings>(configFile, jsonOptions)!;
    }
}
