using HMZ.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Claims;

namespace HMZ.Database.Configurations
{
    public class ClassConfiguration : IEntityTypeConfiguration<ClassRoom>
    {
        public void Configure(EntityTypeBuilder<ClassRoom> builder)
        {
            // default
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Code).IsRequired();
            #region Custom 
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            #endregion Custom
        }
    }
}