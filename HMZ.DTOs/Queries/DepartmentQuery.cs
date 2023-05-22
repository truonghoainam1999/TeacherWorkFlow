using HMZ.Database.Entities.Base;

namespace HMZ.DTOs.Queries
{
    public class DepartmentQuery : BaseEntity
    {
        public String? Name { get; set; }
        public String? Phone { get; set; }
    }
}