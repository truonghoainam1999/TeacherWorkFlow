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
        public Guid? RoomId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? SubjectId { get; set; }
    }
}
