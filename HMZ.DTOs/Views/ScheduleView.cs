using HMZ.Database.Entities.Base;
using HMZ.DTOs.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Views
{
    public class ScheduleView : BaseEntity
    {
        public String? Time { get; set; }
        public String? Day { get; set; }
        public String? Week { get; set; }
        public String? RoomId { get; set; }
    }
}
