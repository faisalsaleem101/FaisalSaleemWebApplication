﻿using DocumentGenerator.PowerPoint;
using FaisalLearningProjectMVC.Data;
using System.Linq;
using Xunit;

namespace UnitTestDocumentGenerator.DocumentGenerator.PowerPoint
{
    public class PowerPointTest
    {
        [Fact]
        public void CreateTable()
        {
            ContextDbService service = new ContextDbService();
            var context = service.GetDbContext();

            var tableGenerator = new TableGeneratorPowerPoint();

            var customers = context.Customers.Select(c => new { c.ContactName, c.CompanyName, c.ContactTitle, c.Address, c.City });

            tableGenerator.Run(customers, "Customers");
        }
    }
}
