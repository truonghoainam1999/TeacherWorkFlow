using HMZ.Database.Entities.Base;

namespace HMZ.Database.Entities
{
    public class ClassRoom : BaseEntity // lớp
    {
        public String? Name { get; set; }

        public virtual List<Schedule>? Schedules { get; set; }
        public virtual List<TaskWork>? Tasks { get; set; }
    }
}