using System.Collections.Generic;
using Newtonsoft.Json;

namespace Commons.Communications.Vehicle
{
    public class GetAllVehicleLocationResponse
    {
        [JsonProperty("Result")] public Dictionary<int, VehicleMovementData> Result { get; set; }
    }
}