using HMZ.DTOs.Models;

namespace HMZ.DTOs.Queries
{
    public class UserQuery
    {
        public String? Email { get; set; } 
        public String? Username { get; set; } 
        public String? Password { get; set; }  
        public String? FirstName { get; set; } 
        public String? LastName { get; set; } 
        public String? CodeDepartment { get; set; }
        public String[]? Role { get; set; } 
        public DateTime? DateOfBirth { get; set; } 
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; } 
        public DateTime? LastActive { get; set; } 
    }
}
