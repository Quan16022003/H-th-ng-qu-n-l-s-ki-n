using Domain.Commons;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).ValueGeneratedOnAdd();

            builder.Property(e => e.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(e => e.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }

}
