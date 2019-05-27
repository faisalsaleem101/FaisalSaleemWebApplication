using DocumentGenerator.Helpers;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DocumentGenerator.Excel
{
    public class TableGeneratorExcel2
    {

        string templateDirectory = @"C:\Users\Faisal Saleem\source\repos\FaisalLearningProjectMVC\Outputs\";

        public string Run<T>(IEnumerable<T> data, string title)
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

            string newTitle = $"{title} {DateTime.Now.ToString().Replace("/", "").Replace(":", "")}.xlsx";
            var fi = new FileInfo(templateDirectory + Path.DirectorySeparatorChar + newTitle);

            pck.SaveAs(fi);

            return newTitle;

        }


    }
}
