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
    class AttendeesConfiguration : BaseEntityConfiguration<Attendees>
    {
        public override void Configure(EntityTypeBuilder<Attendees> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(a => a.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.PrivateReferenceNUmber)
                .HasMaxLength(50);

            builder.Property(a => a.IsCancelled)
                .IsRequired();

            builder.Property(a => a.HasArrived)
                .IsRequired();

            builder.Property(a => a.ArrivalTime)
                .IsRequired(false);
            builder.Navigation(a => a.User).AutoInclude();
            builder.Navigation(a => a.Order).AutoInclude();
            builder.Navigation(a => a.Ticket).AutoInclude();
            builder.Navigation(a => a.Event).AutoInclude();
        }
    }
}
