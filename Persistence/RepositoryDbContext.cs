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
        }
    }
}
