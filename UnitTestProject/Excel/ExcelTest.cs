using DocumentGenerator.Excel;
using FaisalLearningProjectMVC.Data;
using System;
using System.Linq;
using Xunit;

namespace UnitTestDocumentGenerator.Excel
{
    public class ExcelTest
    {

        [Fact]
        public void CreateSpreadSheetTable()
        {
            ContextDbService service = new ContextDbService();
            var context = service.GetDbContext();

            var tableGenerator = new TableGeneratorExcel();

            var fileName = $"Customers {DateTime.Now.ToString().Replace("/", "").Replace(":", "")}.xlsx";

            tableGenerator.Run(context.Customers.ToList(), "Customers", fileName);


        }


    }
}
