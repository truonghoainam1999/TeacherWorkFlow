using HMZ.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMZ.Database.Configurations
{
    public class TaskWorkConfiguration : IEntityTypeConfiguration<TaskWork>
    {
        public void Configure(EntityTypeBuilder<TaskWork> builder)
        {
            // default
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            
            // Foreign Key
            builder.HasOne(x => x.Subject).WithMany(x => x.Tasks).HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany(x => x.Tasks).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Room).WithMany(x => x.Tasks).HasForeignKey(x => x.RoomId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}