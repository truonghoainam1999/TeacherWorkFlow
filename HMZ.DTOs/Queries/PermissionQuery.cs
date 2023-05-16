using HMZ.Database.Entities.Base;
using HMZ.DTOs.Queries.Base;

namespace HMZ.DTOs.Queries
{
    public class PermissionQuery : BaseEntity
    {
        public Guid? RoleId { get; set; }
        public Guid? PermissionId { get; set; }
        public String? PermissionKey { get; set; }
        public String? PermissionValue { get; set; }
        public String? Description { get; set; }
    }
}