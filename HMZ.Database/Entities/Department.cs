using HMZ.Database.Entities.Base;

namespace HMZ.Database.Entities
{
	public class Department : BaseEntity // Khoa
	{
		public String? Name { get; set; }
		public String? Phone { get; set; }

		public virtual List<Subject>? Subjects { get; set; }
		public virtual List<TaskWork>? Tasks { get; set; }
		public virtual List<User>? Users { get; set; }
	}
}
