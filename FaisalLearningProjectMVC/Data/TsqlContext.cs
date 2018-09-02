using FaisalLearningProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.Data
{
    public class TsqlContext : DbContext
    {
        public TsqlContext(DbContextOptions<TsqlContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers", "Sales");
            modelBuilder.Entity<Order>().ToTable("Orders", "Sales");

            modelBuilder.Entity<Customer>().Property(b => b.ID).HasColumnName("custid");
            modelBuilder.Entity<Customer>().Property(b => b.Name).HasColumnName("contactname");
            modelBuilder.Entity<Customer>().Property(b => b.Title).HasColumnName("contacttitle");
            modelBuilder.Entity<Customer>().HasMany(p => p.Orders).WithOne(b => b.Customer);


            modelBuilder.Entity<Order>().Property(b => b.ID).HasColumnName("orderid");
            modelBuilder.Entity<Order>().Property(b => b.CustomerId).HasColumnName("custid");
            modelBuilder.Entity<Order>().HasOne(p => p.Customer).WithMany(b => b.Orders).HasForeignKey(p => p.CustomerId);

        }
    }
}
