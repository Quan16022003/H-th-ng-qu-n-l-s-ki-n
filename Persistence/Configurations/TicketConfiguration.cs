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
    class TicketsConfiguration : BaseEntityConfiguration<Tickets>
    {
        public override void Configure(EntityTypeBuilder<Tickets> builder)
        {
            base.Configure(builder);

            // Configure properties
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Description)
                .HasMaxLength(1000);

            builder.Property(t => t.Price)
                .IsRequired();

            builder.Property(t => t.MaxPerPerson)
                .IsRequired();

            builder.Property(t => t.QuantityAvailable)
                .IsRequired();

            builder.Property(t => t.QuantitySold)
                .IsRequired();

            builder.Property(t => t.Status)
                .IsRequired()
                .HasConversion<int>();

            // Configure relationships
            builder.HasMany(t => t.Attendees)
                .WithOne(a => a.Ticket)
                .HasForeignKey(a => a.TicketId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(t => t.OrderItems)
                .WithOne(ot => ot.Ticket)
                .HasForeignKey(ot => ot.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
