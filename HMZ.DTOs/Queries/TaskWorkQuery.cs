using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMZ.DTOs.Queries.Catalog
{
    public class TaskWorkQuery
    {
        public String? Name { get; set; }
        public String? Description { get; set; }
        public Guid? RoomId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? SubjectId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
