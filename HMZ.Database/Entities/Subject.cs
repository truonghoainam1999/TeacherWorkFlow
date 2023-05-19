using HMZ.Database.Entities.Base;

namespace HMZ.Database.Entities
{
    public class Subject : BaseEntity //Môn học
    {
        public String? Name { get; set; }
        public String? Description { get; set; }

        // Foreign Key
        public Guid? DepartmentId { get; set; }
        public Department? Department { get; set; }

        public List<TaskWork>? Tasks { get; set; }
    }
}