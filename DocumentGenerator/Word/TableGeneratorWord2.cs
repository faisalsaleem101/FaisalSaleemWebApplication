using Novacode;
using System;
using System.Collections.Generic;
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
                document.InsertParagraph("Columns width").FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;

                // Insert a title paragraph.
                var p = document.InsertParagraph("In the following table, the cell's left margin has been removed for rows 2-6 as well as the top/bottom table's borders.").Bold();
                p.Alignment = Alignment.center;
                p.SpacingAfter(40d);

                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 100f, 300f, 200f };
                var t = document.InsertTable(1, columnWidths.Length);

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.Design = TableDesign.TableGrid;
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                // Fill in the columns of the first row in the table.
                for (int i = 0; i < row.Cells.Count; ++i)
                {
                    row.Cells[i].Paragraphs.First().Append("Data " + i);
                }

                // Add rows in the table.
                for (int i = 0; i < 5; i++)
                {
                    var newRow = t.InsertRow();

                    // Fill in the columns of the new rows.
                    for (int j = 0; j < newRow.Cells.Count; ++j)
                    {
                        var newCell = newRow.Cells[j];
                        newCell.Paragraphs.First().Append("Data " + i);
                        // Remove the left margin of the new cells.
                        newCell.MarginLeft = 0;
                    }
                }

                // Set a blank border for the table's top/bottom borders.
                var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                t.SetBorder(TableBorderType.Bottom, blankBorder);
                t.SetBorder(TableBorderType.Top, blankBorder);

                document.Save();
            }





        }


        private static void AddItemToTable(Table table, Row rowPattern, string productName)
        {
            // Gets a random unit price and quantity.
            var unitPrice = Math.Round(rand.NextDouble(), 2);
            var unitQuantity = rand.Next(1, 10);

            // Insert a copy of the rowPattern at the last index in the table.
            var newItem = table.InsertRow(rowPattern, table.RowCount - 1);

            // Replace the default values of the newly inserted row.
            newItem.ReplaceText("%PRODUCT_NAME%", productName);
            newItem.ReplaceText("%PRODUCT_UNITPRICE%", "$ " + unitPrice.ToString("N2"));
            newItem.ReplaceText("%PRODUCT_QUANTITY%", unitQuantity.ToString());
            newItem.ReplaceText("%PRODUCT_TOTALPRICE%", "$ " + (unitPrice * unitQuantity).ToString("N2"));
        }

    }
}