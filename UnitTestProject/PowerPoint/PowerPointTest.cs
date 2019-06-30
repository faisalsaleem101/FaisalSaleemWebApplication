using DocumentGenerator.PowerPoint;
using FaisalLearningProjectMVC.Data;
using System;
using System.Linq;
using Xunit;

namespace UnitTestDocumentGenerator.PowerPoint
{
    public class PowerPointTest
    {
        [Fact]
        public void CreateTable()
        {
            ContextDbService service = new ContextDbService();
            var context = service.GetDbContext();

            var tableGenerator = new TableGeneratorPowerPoint();

            var customers = context.Customers.Select(c => new { c.ContactName, c.CompanyName, c.ContactTitle, c.Address, c.City, c.Country });

            var fileName = $"Customers {DateTime.Now.ToString().Replace("/", "_").Replace(":", "_")}.ppt";

            tableGenerator.Run(customers, "Customers", fileName);
        }
    }
}
