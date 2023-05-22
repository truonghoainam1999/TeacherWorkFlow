using HMZ.Database.Entities;
using HMZ.Database.Entities.Base;

namespace HMZ.DTOs.Views
{
    public class DepartmentView : BaseEntity
    {
        public String? Name { get; set; }
        public String? Phone { get; set; }

        public List<Subject>? Subjects { get; set; }
        public List<TaskWork>? Tasks { get; set; }
        public List<User>? Users { get; set; }
    }
}