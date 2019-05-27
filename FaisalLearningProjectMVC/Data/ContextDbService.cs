using FaisalLearningProjectMVC.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace FaisalLearningProjectMVC.Data
{
    public class ContextDbService
    {
        public ContextDb GetDbContext(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<ContextDb>()
                .UseSqlite(connection)
                .Options;

            // Create the schema in the database
            var context = new ContextDb(options);
            context.Database.EnsureCreated();

            context.Customers.Add(new Customer
            {
                ID = 1,
                CompanyName = "Customer NRZBB",
                ContactName = "Allen, Michael",
                ContactTitle = "Sales Representative",
                Address = "Obere Str. 0123",
                City = "Berlin",
                PostalCode = "10092",
                Country = "Germany",
                Phone = "030-3456789"
            });

            context.Customers.Add(new Customer
            {
                ID = 2,
                CompanyName = "Customer MLTDN",
                ContactName = "Hassall, Mark",
                ContactTitle = "Owner",
                Address = "Avda. de la Constitución 5678",
                City = "México D.F.",
                PostalCode = "10077",
                Country = "Mexico",
                Phone = "(5) 789-0123"
            });

            context.SaveChanges();

            return context;

        }
    }
}
