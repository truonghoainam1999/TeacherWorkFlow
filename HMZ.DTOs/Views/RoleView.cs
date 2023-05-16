using HMZ.Database.Entities.Base;

namespace HMZ.DTOs.Views
{
    public class RoleView : BaseEntity
    {
        public String? RoleName { get; set; }
        public List<PermissionView>? Permissions { get; set; }

    }
}