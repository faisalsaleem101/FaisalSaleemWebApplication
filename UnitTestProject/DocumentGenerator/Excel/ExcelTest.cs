using DocumentGenerator.Excel;
using FaisalLearningProjectMVC.Data;
using System.Linq;
using Xunit;

namespace UnitTestDocumentGenerator.DocumentGenerator.Excel
{
    public class ExcelTest
    {
        string outputDirectory = @"C:\Users\Faisal Saleem\source\repos\FaisalLearningProjectMVC\Outputs\";


        [Fact]
        public void CreateSpreadSheetTable()
        {
            ContextDbService service = new ContextDbService();
            var context = service.GetDbContext();

            var tableGenerator = new TableGeneratorExcel2();

            tableGenerator.Run(context.Customers.ToList(), "Customers", outputDirectory);


        }


    }
}
