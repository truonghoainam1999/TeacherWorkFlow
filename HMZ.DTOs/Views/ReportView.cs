using HMZ.Database.Entities.Base;
using HMZ.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Views
{
	public class ReportView : BaseEntity
	{
		public String? RoomId { get; set; }
		public String? UserId { get; set; }
		public String? SubjectId { get; set; }
		public String? Description { get; set; }
		public String? RoomName { get; set; }
		public String? UserName { get; set; }
		public String? SubjectName { get; set; }
		public string? EndDate { get; set; }
		public Boolean? IsDeadLine { get; set; }
		public Boolean? IsActive { get; set; }
		public Guid? UserCreate { get; set;}
		public Boolean? IsDelay { get; set; }
    }
}
