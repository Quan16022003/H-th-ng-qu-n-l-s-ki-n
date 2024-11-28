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
    class OrdersConfiguration : BaseEntityConfiguration<Orders>
    {
        public override void Configure(EntityTypeBuilder<Orders> builder)
        {
            base.Configure(builder);

            // Configure Customer properties
            builder.Property(o => o.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Email)
                .IsRequired()
                .HasMaxLength(255);

            // Configure OrderStatus as Enum
            builder.Property(o => o.OrderStatus)
                .IsRequired()
                .HasConversion<int>();

            // Configure relationships
            builder.HasMany(o => o.Attendees)
                .WithOne(a => a.Order)
                .HasForeignKey(a => a.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.OrderTickets)
                .WithOne(ot => ot.Order)
                .HasForeignKey(ot => ot.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(o => o.User).AutoInclude();
        }
    }
}
