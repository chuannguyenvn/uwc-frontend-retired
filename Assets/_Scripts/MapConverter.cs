using Newtonsoft.Json;

public static class MapConverter
{
    public static Root Deserialize(string filePath)
    {
        var settings = new JsonSerializerSettings {MissingMemberHandling = MissingMemberHandling.Ignore};
        return JsonConvert.DeserializeObject<Root>(filePath, settings);
    }
}