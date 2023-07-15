using System;
using Models.Types;
using Newtonsoft.Json;

namespace Communications.Employee
{
    public class AddUserProfileRequest
    {
        [JsonProperty("FirstName")] public string FirstName { get; set; }
        [JsonProperty("LastName")] public string LastName { get; set; }
        [JsonProperty("Gender")] public Gender Gender { get; set; }
        [JsonProperty("DateOfBirth")] public DateTime DateOfBirth { get; set; }
        [JsonProperty("Role")] public UserRole Role { get; set; }
    }
}