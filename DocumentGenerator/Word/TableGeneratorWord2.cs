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
                var documentTitle = document.InsertParagraph(title).FontSize(18).SpacingAfter(20d);
                documentTitle.Alignment = Alignment.left;
                documentTitle.Font("Arial");


                // Add a table in a document of 1 row and 3 columns.
                var columnWidths = new float[] { 100f, 300f, 200f };
                var t = document.InsertTable(1, columnWidths.Length);

                // Set the table's column width and background 
                t.SetWidths(columnWidths);
                t.Design = TableDesign.TableGrid;
                t.AutoFit = AutoFit.Contents;

                var row = t.Rows.First();

                var border = new Border() { Color = Color.FromArgb(41, 128, 186) };

                // Fill in the columns of the first row in the table.
                for (int i = 0; i < row.Cells.Count; ++i)
                {
                    row.Cells[i].Paragraphs.First().Append("Data " + i);
                    row.Cells[i].FillColor = Color.FromArgb(41, 128, 186);
                    row.Cells[i].Paragraphs.First().Color(Color.White);

                    SetBorder(border, row.Cells[i]);
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
                        SetBorder(border, newCell);

                        // remove first row of data border on top 
                        if (i == 0)
                            newCell.SetBorder(TableCellBorderType.Top, border);

                    }
                }



                //// Set a blank border for the table's top/bottom borders.
                //var blankBorder = new Border(BorderStyle.Tcbs_none, 0, 0, Color.White);
                //t.SetBorder(TableBorderType.Bottom, blankBorder);
                //t.SetBorder(TableBorderType.Top, blankBorder);

                document.Save();
            }
        }

        private void SetBorder(Border border, Cell cell)
        {
            cell.SetBorder(TableCellBorderType.Bottom, border);
            cell.SetBorder(TableCellBorderType.Top, border);
            cell.SetBorder(TableCellBorderType.Left, border);
            cell.SetBorder(TableCellBorderType.Right, border);
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