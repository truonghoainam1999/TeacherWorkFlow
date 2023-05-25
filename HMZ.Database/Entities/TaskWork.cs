using HMZ.Database.Entities.Base;

namespace HMZ.Database.Entities
{
    public class TaskWork : BaseEntity
    {
        public Guid? RoomId { get; set; }
        public Guid? UserId { get; set; }
        public Guid? SubjectId { get; set; }
        public String? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ClassRoom? ClassRoom { get; set; }
        public virtual User? User { get; set; }
        public virtual Subject? Subject { get; set; }
    }
}