// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Feature
{
    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("properties")] public Properties Properties { get; set; }

    [JsonProperty("geometry")] public Geometry Geometry { get; set; }
}

public class Geometry
{
    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("coordinates")] public List<List<List<double>>> Coordinates { get; set; }
}

public class Properties
{
    [JsonProperty("timestamp")] public DateTime Timestamp { get; set; }

    [JsonProperty("version")] public string Version { get; set; }

    [JsonProperty("changeset")] public string Changeset { get; set; }

    [JsonProperty("user")] public string User { get; set; }

    [JsonProperty("uid")] public string Uid { get; set; }

    [JsonProperty("distance")] public string Distance { get; set; }

    [JsonProperty("from")] public string From { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("name:en")] public string NameEn { get; set; }

    [JsonProperty("name:ko")] public string NameKo { get; set; }

    [JsonProperty("name:vi")] public string NameVi { get; set; }

    [JsonProperty("name:zh")] public string NameZh { get; set; }

    [JsonProperty("network")] public string Network { get; set; }

    [JsonProperty("operator")] public string Operator { get; set; }

    [JsonProperty("public_transport:version")]
    public string PublicTransportVersion { get; set; }

    [JsonProperty("route")] public string Route { get; set; }

    [JsonProperty("to")] public string To { get; set; }

    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("wikidata")] public string Wikidata { get; set; }

    [JsonProperty("wikipedia")] public string Wikipedia { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("admin_level")] public string AdminLevel { get; set; }

    [JsonProperty("boundary")] public string Boundary { get; set; }

    [JsonProperty("name:zh-Hans")] public string NameZhHans { get; set; }

    [JsonProperty("name:zh-Hant")] public string NameZhHant { get; set; }

    [JsonProperty("name:fr")] public string NameFr { get; set; }

    [JsonProperty("name:id")] public string NameId { get; set; }

    [JsonProperty("name:ja")] public string NameJa { get; set; }

    [JsonProperty("leisure")] public string Leisure { get; set; }
}

public class Root
{
    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("features")] public List<Feature> Features { get; set; }
}