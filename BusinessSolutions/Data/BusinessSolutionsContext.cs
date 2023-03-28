using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessSolutions.Models;

namespace BusinessSolutions.Data
{
    public class BusinessSolutionsContext : DbContext
    {
        public BusinessSolutionsContext (DbContextOptions<BusinessSolutionsContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Order { get; set; } 
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Provider> Provider { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().ToTable(nameof(Order));
            modelBuilder.Entity<OrderItem>().ToTable(nameof(OrderItem));
            modelBuilder.Entity<Provider>().ToTable(nameof(Provider));
        }
    }
}
