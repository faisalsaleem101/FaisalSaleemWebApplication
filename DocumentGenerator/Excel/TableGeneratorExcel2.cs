using DocumentGenerator.Helpers;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DocumentGenerator.Excel
{
    public class TableGeneratorExcel2
    {
        public void Run<T>(IEnumerable<T> data, string title, string fileName)
        {
            var pck = new ExcelPackage();

            // Retrieve the data from our data source which is stored as a DataTable.
            DataTable dt = Helper.CreateDataTable(data);

            // get column names with "_" removed
            dt = Helper.GetColumnNamesForDatabable(dt);

            var wsDt = pck.Workbook.Worksheets.Add(title);

            //Load the datatable and set the number formats...
            wsDt.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.Medium9);
            wsDt.Cells[wsDt.Dimension.Address].AutoFitColumns();

            AppConfiguration appConfig = new AppConfiguration();

            var fi = new FileInfo(appConfig.OutputFolderDirectory + Path.DirectorySeparatorChar + fileName);

            pck.SaveAs(fi);
        }
    }
}