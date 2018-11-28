using Aspose.Cells;
using DocumentGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;

namespace DocumentGenerator.Excel
{
    public class TableGeneratorExcel
    {
        public string Run<T>(IEnumerable<T> data, string title)
        {
            // The path to the documents directory.
            var outputDir = Helper.GetOutputPath();

            // Retrieve the data from our data source which is stored as a DataTable.
            DataTable table = Helper.CreateDataTable(data);

            // Create a workbook
            Workbook workbook = new Workbook();
            Worksheet ws = workbook.Worksheets[0];
            Cells cells = workbook.Worksheets[0].Cells;

            // get column names with "_" removed 
            List<string> columnNames = Helper.GetColumnNames(table);

            // Generate Columns 
            GenerateColumns(cells, columnNames);

            // Generate rows and add data
            GenerateRows(table, cells);

            ws.AutoFitRows();
            ws.AutoFitColumns();

            workbook.Save($"{outputDir}//{title}.xlsx");

            return $"{title}.xlsx";
        }

        private static void GenerateRows(DataTable table, Cells cells)
        {
            //Generate Rows
            int rowCounter = 1;
            foreach (DataRow row in table.Rows)
            {
                int colCounter = 0;
                foreach (var item in row.ItemArray)
                {
                    // insert value
                    cells[rowCounter, colCounter].PutValue(item);
                    colCounter++;
                }
                rowCounter++;
            }
        }

        private static void GenerateColumns(Cells cells, List<string> columnNames)
        {
            int col = 0;
            foreach (var column in columnNames)
            {
                // insert column name
                cells[0, col].PutValue(column);

                // change column style
                Style cellStyle = cells[0, col].GetStyle();
                cellStyle.Font.Color = Color.White;
                cellStyle.ForegroundColor = Color.FromArgb(102, 102, 102);
                cellStyle.Pattern = BackgroundType.Solid;
                cells[0, col].SetStyle(cellStyle);

                col++;
            }
        }
    }
}
