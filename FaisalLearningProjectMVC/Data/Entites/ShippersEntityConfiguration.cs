using FaisalLearningProjectMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaisalLearningProjectMVC.Data.Entites
{
    public class ShippersEntityConfiguration : IEntityTypeConfiguration<Shipper>
    {
        public void Configure(EntityTypeBuilder<Shipper> modelBuilder)
        {
            modelBuilder.Property(b => b.ID).HasColumnName("shipperid");
            modelBuilder.Property(b => b.CompanyName).HasColumnName("companyname");
        }
    }
}
