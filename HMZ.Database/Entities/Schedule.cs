using HMZ.Database.Entities.Base;

namespace HMZ.Database.Entities
{
    public class Schedule : BaseEntity // Thời khóa biểu
    {
        public String? Time { get; set; }
        public String? Day { get; set; }
        public String? Week { get; set; }
        // Foreign Key
        public Guid? RoomId { get; set; }
        public ClassRoom? Room { get; set; }
    }
}