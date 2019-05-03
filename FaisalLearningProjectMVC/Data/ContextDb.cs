using FaisalLearningProjectMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace FaisalLearningProjectMVC.Data
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers", "Sales");
            modelBuilder.Entity<Order>().ToTable("Orders", "Sales");

            modelBuilder.Entity<Customer>().HasMany(p => p.Orders).WithOne(b => b.Customer);
            modelBuilder.Entity<Customer>().Property(b => b.ID).HasColumnName("custid");
            modelBuilder.Entity<Customer>().Property(b => b.ContactName).HasColumnName("contactname");
            modelBuilder.Entity<Customer>().Property(b => b.ContactTitle).HasColumnName("contacttitle");
            modelBuilder.Entity<Customer>().Property(b => b.Address).HasColumnName("address");
            modelBuilder.Entity<Customer>().Property(b => b.City).HasColumnName("city");
            modelBuilder.Entity<Customer>().Property(b => b.Region).HasColumnName("region");
            modelBuilder.Entity<Customer>().Property(b => b.PostalCode).HasColumnName("postalcode");
            modelBuilder.Entity<Customer>().Property(b => b.Country).HasColumnName("country");
            modelBuilder.Entity<Customer>().Property(b => b.Phone).HasColumnName("phone");
            modelBuilder.Entity<Customer>().Property(b => b.Fax).HasColumnName("fax");
            modelBuilder.Entity<Customer>().Property(b => b.IsActive).HasColumnName("IsActive");

            modelBuilder.Entity<Order>().HasOne(p => p.Customer).WithMany(b => b.Orders).HasForeignKey(p => p.CustomerId);
            modelBuilder.Entity<Order>().Property(b => b.ID).HasColumnName("orderid");
            modelBuilder.Entity<Order>().Property(b => b.CustomerId).HasColumnName("custid");
            modelBuilder.Entity<Order>().Property(b => b.EmployeeId).HasColumnName("empid");
            modelBuilder.Entity<Order>().Property(b => b.ShipperId).HasColumnName("shipperid");
            modelBuilder.Entity<Order>().Property(b => b.OrderDate).HasColumnName("orderdate");
            modelBuilder.Entity<Order>().Property(b => b.RequiredDate).HasColumnName("requireddate");
            modelBuilder.Entity<Order>().Property(b => b.ShippedDate).HasColumnName("shippeddate");
            modelBuilder.Entity<Order>().Property(b => b.Freight).HasColumnName("freight");
            modelBuilder.Entity<Order>().Property(b => b.ShipName).HasColumnName("shipname");
            modelBuilder.Entity<Order>().Property(b => b.ShipAddress).HasColumnName("shipaddress");
            modelBuilder.Entity<Order>().Property(b => b.ShipCity).HasColumnName("shipcity");
            modelBuilder.Entity<Order>().Property(b => b.ShipRegion).HasColumnName("shipregion");
            modelBuilder.Entity<Order>().Property(b => b.ShipPostalCode).HasColumnName("shippostalcode");
            modelBuilder.Entity<Order>().Property(b => b.ShipCountry).HasColumnName("shipcountry");

        }
    }
}
