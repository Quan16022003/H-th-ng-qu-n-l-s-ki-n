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
    class OrderItemsConfiguration : BaseEntityConfiguration<OrderItems>
    {
        public override void Configure(EntityTypeBuilder<OrderItems> builder)
        {
            base.Configure(builder);

            // Configure properties
            builder.Property(oi => oi.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(oi => oi.Quantity)
                .IsRequired();

            builder.Property(oi => oi.UnitPrice)
                .IsRequired();

            // Configure relationship
            builder.HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
