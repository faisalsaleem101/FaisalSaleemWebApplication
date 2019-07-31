using FaisalLearningProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaisalLearningProjectMVC.Data.Entities
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> modelBuilder)
        {
            modelBuilder.ToTable("Orders", "Sales");

            modelBuilder.Property(b => b.ID).HasColumnName("orderid");
            modelBuilder.Property(b => b.CustomerId).HasColumnName("custid");
            modelBuilder.Property(b => b.EmployeeId).HasColumnName("empid");
            modelBuilder.Property(b => b.ShipperId).HasColumnName("shipperid");
            modelBuilder.Property(b => b.OrderDate).HasColumnName("orderdate");
            modelBuilder.Property(b => b.RequiredDate).HasColumnName("requireddate");
            modelBuilder.Property(b => b.ShippedDate).HasColumnName("shippeddate");
            modelBuilder.Property(b => b.Freight).HasColumnName("freight");
            modelBuilder.Property(b => b.ShipName).HasColumnName("shipname");
            modelBuilder.Property(b => b.ShipAddress).HasColumnName("shipaddress");
            modelBuilder.Property(b => b.ShipCity).HasColumnName("shipcity");
            modelBuilder.Property(b => b.ShipRegion).HasColumnName("shipregion");
            modelBuilder.Property(b => b.ShipPostalCode).HasColumnName("shippostalcode");
            modelBuilder.Property(b => b.ShipCountry).HasColumnName("shipcountry");
            modelBuilder.Property(b => b.IsActive).HasColumnName("IsActive");

            modelBuilder.HasOne(p => p.Customer).WithMany(b => b.Orders).HasForeignKey(p => p.CustomerId);
            modelBuilder.HasOne(s => s.Shipper).WithMany(b => b.Orders).HasForeignKey(p => p.ShipperId);

        }
    }
}
