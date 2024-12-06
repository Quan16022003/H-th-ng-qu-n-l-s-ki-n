using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    class EventConfiguration : BaseEntityConfiguration<Events>
    {
        public override void Configure(EntityTypeBuilder<Events> builder)
        { 
            base.Configure(builder);

            // Configure properties
            builder.Property(e => e.Slug)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.Description)
                .HasMaxLength(2000);

            builder.Property(e => e.IsPublic)
                .HasDefaultValue(false);

            builder.Property(e => e.StartDate)
                .HasColumnType("date");

            builder.Property(e => e.EndDate)
                .HasColumnType("date");

            builder.Property(e => e.VenueName)
                .HasMaxLength(255);

            builder.Property(e => e.City)
                .HasMaxLength(100);

            builder.Property(e => e.District)
                .HasMaxLength(100);

            builder.Property(e => e.Ward)
                .HasMaxLength(100);

            builder.Property(e => e.Address)
                .HasMaxLength(255);

            builder.Property(e => e.PostalCode)
                .HasMaxLength(20);

            builder.Property(e => e.Latitude)
                .HasColumnType("decimal(10, 8)");

            builder.Property(e => e.Longitude)
                .HasColumnType("decimal(11, 8)");

            builder.Property(e => e.PlaceId)
                .HasMaxLength(100);

            builder.Property(e => e.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(e => e.WebsiteUrl)
                .HasMaxLength(2048);

            builder.Property(e => e.ThumbnailUrl)
                .HasMaxLength(2048);

            builder.Property(e => e.CoverUrl)
                .HasMaxLength(2048);

            // Configure relationships
            builder.HasMany(e => e.Tickets)
                .WithOne(t => t.Event)
                .HasForeignKey(t => t.EventId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(e => e.Orders)
                .WithOne(o => o.Event)
                .HasForeignKey(o => o.EventId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(e => e.Attendees)
                .WithOne(a => a.Event)
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Navigation(x => x.Organizer).AutoInclude();
            builder.Navigation(x => x.CategoryEvent).AutoInclude();
        }
    }
}
