using HMZ.Database.Entities.Base;

namespace HMZ.Database.Entities
{
    public class ClassRoom : BaseEntity // lớp
    {
        public String? Name { get; set; }
        public String? Code { get; set; }

        public List<Schedule>? Schedules { get; set; }
        public List<TaskWork>? Tasks { get; set; }
    }
}