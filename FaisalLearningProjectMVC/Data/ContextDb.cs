using FaisalLearningProjectMVC.Data.Entites;
using FaisalLearningProjectMVC.Data.Entities;
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
            modelBuilder.ApplyConfiguration(new CustomerEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ShippersEntityConfiguration());
        }
    }
}
