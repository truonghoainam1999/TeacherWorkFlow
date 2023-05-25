using HMZ.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Fillters
{
	public class ReportFilter
	{
		public Guid? RoomId { get; set; }
		public Guid? UserId { get; set; }
		public Guid? SubjectId { get; set; }
		public string? Description { get; set; }

		public bool? IsDeadline { get; set; }
		public bool? IsActive { get; set; }
		public RangeFilter<DateTime?>? CreatedAt { get; set; }
		public RangeFilter<DateTime?>? UpdatedAt { get; set; }
		public bool? IsDelayed { get; set; } 
		public bool? IsToday { get; set; } 
	}
}
