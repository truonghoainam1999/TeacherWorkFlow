using HMZ.DTOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Fillters
{
    public class ClassRoomFilter
    {
        public Guid? SubjectId { get; set; }
        public Guid? TaskWorkId { get; set; }
        public String? RoomName { get; set; }
        public String? Description { get; set; }

        public Boolean? IsActive { get; set; }
        public RangeFilter<DateTime?>? CreatedAt { get; set; }
        public RangeFilter<DateTime?>? UpdatedAt { get; set; }
    }
}
