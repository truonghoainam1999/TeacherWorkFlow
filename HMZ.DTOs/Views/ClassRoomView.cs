using HMZ.Database.Entities.Base;
using HMZ.DTOs.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Views
{
    public class ClassRoomView : BaseEntity
    {
        public String? Name { get; set; }
        public String? Code { get; set; }
        public List<ScheduleView>? ScheduleViews { get; set; }
        public List<TaskWorkView>? TaskViews { get; set; }
        public List<UserView>? UserViews { get; set; }
    }
}
