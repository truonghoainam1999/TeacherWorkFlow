using HMZ.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HMZ.Database.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            // default
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            #region Custom 
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(100);
            #endregion Custom

            // Foreign Key
            builder.HasOne(x => x.Department).WithMany(x => x.Subjects).HasForeignKey(x => x.DepartmentId);

        }
    }
}