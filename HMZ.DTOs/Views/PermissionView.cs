using HMZ.Database.Entities.Base;

namespace HMZ.DTOs.Views
{
    public class PermissionView : BaseEntity
    {
        public Guid? RoleId { get; set; }
        public String? RoleName { get; set; }
        public Guid? PermissionId { get; set; }
        public String? PermissionKey { get; set; }
        public String? PermissionValue { get; set; }
        public String? Description { get; set; }
    }
}
