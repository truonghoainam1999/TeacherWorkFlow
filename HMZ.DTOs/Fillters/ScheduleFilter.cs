using HMZ.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Fillters
{
    public class ScheduleFilter
    {
        public Guid? RoomId { get; set; }
        public String? Time { get; set; }
        public String? Day { get; set; }
        public String? Week { get; set; }
        public String? Description { get; set; }

        public Boolean? IsActive { get; set; }
        public RangeFilter<DateTime?>? CreatedAt { get; set; }
        public RangeFilter<DateTime?>? UpdatedAt { get; set; }
    }
}
