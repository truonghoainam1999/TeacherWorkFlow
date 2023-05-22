using HMZ.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Fillters
{
    public class TaskWorkFilter
    {
        public Guid? RoomId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? SubjectId { get; set; }
        public String? TaskWorkName { get; set; }
        public String? Description { get; set; }

        public Boolean? IsActive { get; set; }
        public RangeFilter<DateTime?>? CreatedAt { get; set; }
        public RangeFilter<DateTime?>? UpdatedAt { get; set; }
    }
}
