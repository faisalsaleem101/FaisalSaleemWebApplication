using FaisalLearningProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaisalLearningProjectMVC.Data
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> modelBuilder)
        {
            modelBuilder.ToTable("Customers", "Sales");

            modelBuilder.Property(b => b.ID).HasColumnName("custid");
            modelBuilder.Property(b => b.ContactName).HasColumnName("contactname");
            modelBuilder.Property(b => b.ContactTitle).HasColumnName("contacttitle");
            modelBuilder.Property(b => b.Address).HasColumnName("address");
            modelBuilder.Property(b => b.City).HasColumnName("city");
            modelBuilder.Property(b => b.Region).HasColumnName("region");
            modelBuilder.Property(b => b.PostalCode).HasColumnName("postalcode");
            modelBuilder.Property(b => b.Country).HasColumnName("country");
            modelBuilder.Property(b => b.Phone).HasColumnName("phone");
            modelBuilder.Property(b => b.Fax).HasColumnName("fax");
            modelBuilder.Property(b => b.IsActive).HasColumnName("IsActive");

            modelBuilder.HasMany(p => p.Orders).WithOne(b => b.Customer);
        }
    }
}
