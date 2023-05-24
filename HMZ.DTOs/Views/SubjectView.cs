using HMZ.Database.Entities.Base;

namespace HMZ.DTOs.Views
{
    public class SubjectView : BaseEntity
    {
        public String? IdString { get; set; }
        public String? Name { get; set; }
        public String? Description { get; set; }
        public String? DepartmentId { get; set; }
        public String? DepartmentName { get; set; }
    }
}