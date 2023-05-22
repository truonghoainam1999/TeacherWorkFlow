using HMZ.Database.Entities.Base;

namespace HMZ.DTOs.Queries
{
    public class SubjectQuery :BaseEntity
    {
        public String? Name { get; set; }
        public String? Description { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}