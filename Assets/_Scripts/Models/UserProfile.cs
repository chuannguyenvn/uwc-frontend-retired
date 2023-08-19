using System;
using Commons.Types;

namespace Models
{
    public class UserProfile : IndexedEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public UserRole Role { get; set; }
    }
}