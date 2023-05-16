using Microsoft.AspNetCore.Identity;

namespace HMZ.Database.Entities
{
    public class Role : IdentityRole<Guid>
    {
        public List<UserRole>? UserRoles { get; set; }
        public List<RolePermission>? RolePermissions { get; set; }

    }
}
