using Microsoft.EntityFrameworkCore;

namespace FaisalLearningProjectMVC.Data
{
    public class ContextDbService
    {
        public ContextDb GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ContextDb>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TSQLV4Development;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            // Create the schema in the database
            var context = new ContextDb(options);
            context.Database.EnsureCreated();

            return context;

        }
    }
}
