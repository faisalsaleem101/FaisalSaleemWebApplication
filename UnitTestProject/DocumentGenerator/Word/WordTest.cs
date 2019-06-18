using DocumentGenerator.Word;
using FaisalLearningProjectMVC.Data;
using System.Linq;
using Xunit;

namespace UnitTestDocumentGenerator.DocumentGenerator.Word
{
    public class WordTest
    {
        [Fact]
        public void GenerateTable()
        {
            ContextDbService service = new ContextDbService();
            var context = service.GetDbContext();

            var tableGenerator = new TableGeneratorWord2();

            tableGenerator.Run(context.Customers.ToList(), "Customers");


        }
    }
}
