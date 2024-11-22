using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class RepositoryDbContext : IdentityDbContext<ApplicationUser>
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options) : base(options)
        {
        }
        public DbSet<CategoryEvents> CategoryEvents { get; set; }

        public DbSet<Events> Events { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<OrderTicket> OrderTickets { get; set; }
        public DbSet<Attendees> Attendees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);
            SampleData.InitializeAsync(modelBuilder);

            //ConfigEventsEntityReference(modelBuilder);
            //ConfigAttendeesEntityReference(modelBuilder);
            //ConfigOrdersEntityReference(modelBuilder);
            //ConfigOrderTicketsEntityReference(modelBuilder);
            //ConfigTicketsEntityReference(modelBuilder);
        }

        //private void ConfigEventsEntityReference(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Events>().Navigation(e => e.Attendees).AutoInclude();
        //    modelBuilder.Entity<Events>().Navigation(e => e.Orders).AutoInclude();
        //    modelBuilder.Entity<Events>().Navigation(e => e.CategoryEvent).AutoInclude();
        //    modelBuilder.Entity<Events>().Navigation(e => e.Organizer).AutoInclude();
        //    modelBuilder.Entity<Events>().Navigation(e => e.Tickets).AutoInclude(); 
        //}

        //private void ConfigAttendeesEntityReference(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Attendees>().Navigation(e => e.User).AutoInclude();
        //    modelBuilder.Entity<Attendees>().Navigation(e => e.Order).AutoInclude();
        //    modelBuilder.Entity<Attendees>().Navigation(e => e.Ticket).AutoInclude();
        //    modelBuilder.Entity<Attendees>().Navigation(e => e.Event).AutoInclude();
        //}

        //private void ConfigUsersEntityReference(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ApplicationUser>().Navigation(e => e.Events).AutoInclude();
        //    modelBuilder.Entity<ApplicationUser>().Navigation(e => e.Orders).AutoInclude();
        //    modelBuilder.Entity<ApplicationUser>().Navigation(e => e.Attendees).AutoInclude();
        //}

        //private void ConfigOrdersEntityReference(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Orders>().Navigation(e => e.User).AutoInclude();
        //    modelBuilder.Entity<Orders>().Navigation(e => e.Event).AutoInclude();
        //    modelBuilder.Entity<Orders>().Navigation(e => e.Attendees).AutoInclude();
        //    modelBuilder.Entity<Orders>().Navigation(e => e.OrderItems).AutoInclude();
        //    modelBuilder.Entity<Orders>().Navigation(e => e.OrderTickets).AutoInclude();

        //    modelBuilder.Entity<OrderItems>().Navigation(e => e.Order).AutoInclude();
        //}

        //private void ConfigOrderTicketsEntityReference(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<OrderTicket>().Navigation(e => e.Order).AutoInclude();
        //    modelBuilder.Entity<OrderTicket>().Navigation(e => e.Ticket).AutoInclude();
        //}
        //private void ConfigTicketsEntityReference(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Tickets>().Navigation(e => e.Event).AutoInclude();
        //    modelBuilder.Entity<Tickets>().Navigation(e => e.Attendees).AutoInclude();
        //    modelBuilder.Entity<Tickets>().Navigation(e => e.OrderTickets).AutoInclude();
        //}

    }
}
