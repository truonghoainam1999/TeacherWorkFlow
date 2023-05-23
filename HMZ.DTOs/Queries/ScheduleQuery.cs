using HMZ.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Queries.Catalog
{
    public class ScheduleQuery : BaseEntity
    {
        public String? Time { get; set; }
        public String? Date { get; set; }
        public String? Week { get; set; }
        public String? RoomId { get; set; }
    }
}
