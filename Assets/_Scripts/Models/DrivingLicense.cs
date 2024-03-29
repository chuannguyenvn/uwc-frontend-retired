using System;

namespace Models
{
    public class DrivingLicense : IndexedEntity
    {
        public DriverProfile DriverProfile { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssuePlace { get; set; }
        public string Type { get; set; }
    }
}