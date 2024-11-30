using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class CategoryEventConfiguration : BaseEntityConfiguration<CategoryEvents>
    {
        public override void Configure(EntityTypeBuilder<CategoryEvents> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Description)
                .HasMaxLength(1000);

            builder.Property(e => e.ThumbnailUrl)
                .HasMaxLength(2048);

            builder.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasMany(c => c.Events)
                .WithOne(e => e.CategoryEvent)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
