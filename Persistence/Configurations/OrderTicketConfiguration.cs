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
    class OrderTicketConfiguration : BaseEntityConfiguration<OrderTicket>
    {
        public override void Configure(EntityTypeBuilder<OrderTicket> builder)
        {
            base.Configure(builder);
            builder.Navigation(ot => ot.Order).AutoInclude();
            builder.Navigation(ot => ot.Ticket).AutoInclude();
        }
    }
}
