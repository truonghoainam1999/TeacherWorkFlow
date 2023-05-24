using HMZ.Database.Entities.Base;
using HMZ.DTOs.Queries.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Views
{
    public class TaskWorkView : BaseEntity
    {
        public String? RoomId { get; set; }
        public String? RoomName { get; set; }
        public String? UserId { get; set; }
        public String? Username { get; set; }
        public String? SubjectId { get; set; }
        public String? SubjectName { get; set; }
    }
}
