using System;
using Newtonsoft.Json;

namespace Communications.Vehicle
{
    public class UpdateDrivingLicenseRequest
    {
        [JsonProperty("Id")] public int Id { get; set; }
        [JsonProperty("IssueDate")] public DateTime IssueDate { get; set; }
        [JsonProperty("IssuePlace")] public string IssuePlace { get; set; }
        [JsonProperty("Owner")] public int Owner { get; set; }
        [JsonProperty("Type")] public string Type { get; set; }
    }
}