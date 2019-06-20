using DocumentGenerator.Word;
using FaisalLearningProjectMVC.Data;
using System;
using System.Linq;
using Xunit;

namespace UnitTestDocumentGenerator.Word
{
    public class WordTest
    {
        [Fact]
        public void GenerateTable()
        {
            ContextDbService service = new ContextDbService();
            var context = service.GetDbContext();

            // we need to use anonoymus type so set the custom label names
            var Customers = context.Customers.Select(x => new
            {
                FullName = x.ContactName,
                Company = x.CompanyName,
                JobTitle = x.ContactTitle,
                x.Address,
                x.City,
            }).ToList();

            var tableGenerator = new TableGeneratorWord2();

            var fileName = $"Customers {DateTime.Now.ToString().Replace("/", "").Replace(":", "")}.docx";

            tableGenerator.Run(Customers, "Customers", fileName);


        }
    }
}
