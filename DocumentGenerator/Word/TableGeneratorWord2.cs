using DocumentGenerator.Helpers;
using Novacode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;


namespace DocumentGenerator.Word
{
    public class TableGeneratorWord2
    {
        private static Random rand = new Random();

        public void Run<T>(IEnumerable<T> data, string title)
        {
            AppConfiguration config = new AppConfiguration();

            using (DocX document = DocX.Create(config.OutputFolderDirectory + @"\\CreateTableFromTemplate.docx"))
            {
                // Add a title
                var documentTitle = document.InsertParagraph(title).FontSize(18).SpacingAfter(20d);
                documentTitle.Alignment = Alignment.center;
                documentTitle.Font("Arial");

                // Add a table in a document of 1 row and x columns.
                var columnsNo = data.FirstOrDefault()?.GetType()?.GetProperties()?.Length ?? 0;
                var t = document.InsertTable(1, columnsNo);

                var columnNames = new List<string>();
                var properties = data.FirstOrDefault()?.GetType()?.GetProperties();

                foreach (var property in properties)
                {
                    var propertyName = string.Concat(property.Name.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
                    columnNames.Add(propertyName);
                }

                // Set the table's properties 
                t.Alignment = Alignment.center;
                t.Design = TableDesign.TableGrid;
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();
                var border = new Border() { Color = Color.FromArgb(41, 128, 186) };

                SetColumnNamesInTable(columnNames, row, border);
                SetDataInTable(data, t, border);

                document.Save();
            }
        }

        private void SetColumnNamesInTable(List<string> columnNames, Row row, Border border)
        {
            // Fill in the columns of the first row in the table.
            for (int i = 0; i < columnNames.Count; ++i)
            {
                row.Cells[i].Paragraphs.First().Append(columnNames[i]);
                row.Cells[i].FillColor = Color.FromArgb(41, 128, 186);
                row.Cells[i].Paragraphs.First().Color(Color.White);

                SetBorder(border, row.Cells[i]);
            }
        }

        private void SetDataInTable<T>(IEnumerable<T> data, Table t, Border border)
        {
            // Retrieve the data from our data source which is stored as a DataTable.
            DataTable table = Helper.CreateDataTable(data);

            // Add rows in the table.
            for (int r = 0; r < table.Rows.Count; r++)
            {
                var newRow = t.InsertRow();

                // Fill in the columns of the new rows.
                for (int c = 0; c < table.Columns.Count; c++)
                {
                    var newCell = newRow.Cells[c];
                    newCell.Paragraphs.First().Append(table.Rows[r][c].ToString());
                    SetBorder(border, newCell);

                    // remove first row of data border on top 
                    if (r == 0)
                        newCell.SetBorder(TableCellBorderType.Top, border);

                }
            }
        }


        private void SetBorder(Border border, Cell cell)
        {
            cell.SetBorder(TableCellBorderType.Bottom, border);
            cell.SetBorder(TableCellBorderType.Top, border);
            cell.SetBorder(TableCellBorderType.Left, border);
            cell.SetBorder(TableCellBorderType.Right, border);
        }

    }
}