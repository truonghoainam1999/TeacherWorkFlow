using HMZ.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMZ.Database.Configurations
{
    public class ScheuleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            // default
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            #region Custom 
            builder.Property(x => x.Time).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Day).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Week).HasMaxLength(100).IsRequired();
            #endregion Custom
            // Foreign Key
            builder.HasOne(x => x.Room).WithMany(x => x.Schedules).HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}