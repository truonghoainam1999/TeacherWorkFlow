using HMZ.Database.Entities.Base;

namespace HMZ.Database.Entities
{
    public class TaskWork : BaseEntity
    {
        public Guid? RoomId { get; set; }
        public ClassRoom? ClassRoom { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public Guid? SubjectId { get; set; }
        public Subject? Subject { get; set; }
    }
}