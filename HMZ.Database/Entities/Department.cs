using HMZ.Database.Entities.Base;

namespace HMZ.Database.Entities
{
	public class Department : BaseEntity
	{
		public String? Name { get; set; }
		public String? Phone { get; set; }

		public List<Subject>? Subjects { get; set; }
		public List<TaskWork>? Tasks { get; set; }
	}
}
