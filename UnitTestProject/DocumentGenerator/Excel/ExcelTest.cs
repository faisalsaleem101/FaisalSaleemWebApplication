using DocumentGenerator.Excel;
using FaisalLearningProjectMVC.Data;
using Microsoft.Data.Sqlite;
using System.Linq;
using Xunit;

namespace UnitTestDocumentGenerator.DocumentGenerator.Excel
{
    public class ExcelTest
    {

        [Fact]
        public void CreateSpreadSheetTable()
        {
            // In-memory database only exists while the connection is open
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            try
            {
                ContextDbService service = new ContextDbService();
                var context = service.GetDbContext(connection);

                var tableGenerator = new TableGeneratorExcel2();

                tableGenerator.Run(context.Customers.ToList(), "Customers");

            }
            finally
            {
                connection.Close();
            }
        }


    }
}
