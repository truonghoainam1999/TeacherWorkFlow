using HMZ.Database.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Queries.Catalog
{
    public class TaskWorkQuery : BaseEntity
    {
        public String? RoomId { get; set; }
        public String? UserId { get; set; }
        public String? SubjectId { get; set; }
    }
}
