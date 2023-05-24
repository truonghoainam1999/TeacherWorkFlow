using HMZ.Database.Common;

namespace HMZ.Database.Entities.Base
{
    public class BaseEntity
    {
        public Guid? Id { get; set; } = Guid.NewGuid();
        public String? Code { get; set; } = HelperCommon.GenerateCode(10, "HMZ");
		public String? CreatedBy { get; set; } = "System";
        public String? UpdatedBy { get; set; } = null;
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public Boolean? IsActive { get; set; } = true;

        
    }
}
