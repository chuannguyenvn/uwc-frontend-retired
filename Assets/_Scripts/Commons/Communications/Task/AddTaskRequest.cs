using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Commons.Communications.Task
{
    public class AddTaskRequest
    {
        [JsonProperty("Date")] public DateTime Date { get; set; }
        [JsonProperty("SupervisorId")] public int SupervisorId { get; set; }
        [JsonProperty("WorkerId")] public int WorkerId { get; set; }
        [JsonProperty("McpIds")] public List<int> McpIds { get; set; } = new();
    }
}