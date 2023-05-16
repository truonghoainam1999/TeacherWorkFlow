using Microsoft.AspNetCore.Identity;

namespace HMZ.Database.Entities
{
    public class User : IdentityUser<Guid>
    {
        public String? FirstName { get; set; }
        public String? LastName { get; set; }
        public String? Image { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Foreign key
        public List<RolePermission>? UserPermissions { get; set; }
        public List<TaskWork>? Tasks { get; set; }

        // Base
        public Boolean? IsActive { get; set; }
        public String? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
