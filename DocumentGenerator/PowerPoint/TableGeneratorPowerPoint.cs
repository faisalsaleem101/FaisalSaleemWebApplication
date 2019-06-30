using DocumentGenerator.Helpers;
using Spire.Presentation;
using Spire.Presentation.Drawing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;

namespace DocumentGenerator.PowerPoint
{
    public class TableGeneratorPowerPoint
    {
        const int rowsPerSlide = 11;
        const string title = "Customers";

        public void Run<T>(IEnumerable<T> data, string title, string fileName)
        {

            Presentation presentation = new Presentation();

            int totalNoOfRows = data.ToList().Count();
            double noOfSlidesDouble = Convert.ToDouble(totalNoOfRows) / Convert.ToDouble(rowsPerSlide);

            // get closest upper number from the double
            double noOfSlides = Math.Ceiling(noOfSlidesDouble);

            // use reflection to get the number of columns in the list
            int noOfColumns = data.FirstOrDefault()?.GetType()?.GetProperties()?.Length ?? 0;
            // use reflection to get the column names
            var columnNames = data.FirstOrDefault()?.GetType()?.GetProperties()?.Select(c => c.Name).ToList();
            // add space between capital letters
            columnNames = columnNames.Select(c => (string.Concat(c.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' '))).ToList();

            int rowCounter = 0;

            // create no of slides needed is spire (1 already exists from the start)
            for (int i = 1; i < noOfSlides; i++)
                presentation.Slides.Append();

            // foreach slide starting at first one
            for (int i = 0; i < noOfSlides; i++)
            {
                CreateTitle(presentation, i, title);

                // number of data rows left to insert
                int noOfRowsLeft = totalNoOfRows - rowCounter;

                // get number of rows to insert either rowsPerSlide or the remaining rows e.g 4
                int rowsPerCurrentSlide = noOfRowsLeft > rowsPerSlide ? rowsPerSlide : noOfRowsLeft;

                // widths and heighs for table
                var widthsAndHeights = GetCellWidthsAndHeights(rowsPerCurrentSlide, noOfColumns);

                // needed to calculate column space
                int y = noOfColumns * 50;
                // create table
                ITable table = presentation.Slides[i].Shapes.AppendTable(presentation.SlideSize.Size.Width / 2 - y, 40, widthsAndHeights.widths, widthsAndHeights.heights);

                // insert column names
                InsertColumnNames(columnNames, table);

                // create databale to work with for data insert
                DataTable dataTable = Helper.CreateDataTable(data);

                // insert data
                rowCounter = InsertData(noOfColumns, rowCounter, rowsPerCurrentSlide, table, dataTable);
            }

            // get file path configuration
            AppConfiguration config = new AppConfiguration();
            var filePath = config.OutputFolderDirectory + $@"\\{fileName}";

            // save file
            presentation.SaveToFile(filePath, FileFormat.Pptx2010);
        }

        private static void CreateTitle(Presentation presentation, int slideNumber, string title)
        {
            IAutoShape shape = presentation.Slides[slideNumber].Shapes.AppendShape(ShapeType.Rectangle, new RectangleF(290, 10, 150, 20));

            shape.Fill.FillType = Spire.Presentation.Drawing.FillFormatType.None;

            //set the LineColor
            shape.ShapeStyle.LineColor.Color = Color.White;

            //set the color and fill style
            shape.AppendTextFrame(title);

            //set the color of text in shape
            TextRange textRange = shape.TextFrame.TextRange;
            textRange.Fill.FillType = Spire.Presentation.Drawing.FillFormatType.Solid;
            textRange.Fill.SolidColor.Color = Color.Black;

            //set the Font of text in shape
            textRange.FontHeight = 18;
            textRange.LatinFont = new TextFont("Arial");
        }

        private static int InsertData(int noOfColumns, int rowCounter, int rowsPerCurrentSlide, ITable table, DataTable dataTable)
        {
            for (int rowNumber = 0; rowNumber < rowsPerCurrentSlide; rowNumber++)
            {
                for (int c = 0; c < noOfColumns; c++)
                {
                    table[c, rowNumber + 1].TextFrame.Text = dataTable.Rows[rowCounter][c]?.ToString() ?? "";
                    table[c, rowNumber + 1].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Calibri");
                    table[c, rowNumber + 1].TextFrame.Paragraphs[0].TextRanges[0].FontHeight = 12;
                }

                var tableRow = table.TableRows[rowNumber + 1];

                //set cell colour
                if (rowCounter % 2 == 0)
                    SetColor(tableRow, 245, 245, 245);
                else
                    SetColor(tableRow, 255, 255, 255);

                rowCounter++;

                if (rowNumber == rowsPerSlide - 1)
                    continue;

            }

            return rowCounter;
        }

        private static void InsertColumnNames(List<string> columnNames, ITable table)
        {
            // insert column names on first row
            for (int c = 0; c < columnNames.Count; c++)
            {
                table[c, 0].TextFrame.Text = columnNames[c];
                table[c, 0].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Calibri");
                table[c, 0].TextFrame.Paragraphs[0].TextRanges[0].FontHeight = 12;
            }

            var tableRow = table.TableRows[0];
            SetColor(tableRow, 41, 128, 186);
        }

        private static void SetColor(TableRow tableRow, byte r, byte g, byte b)
        {
            foreach (Cell cell in tableRow)
            {
                // set cell colour in RGB format 
                cell.FillFormat.FillType = FillFormatType.Solid;
                cell.FillFormat.SolidColor.R = r;
                cell.FillFormat.SolidColor.G = g;
                cell.FillFormat.SolidColor.B = b;
            }
        }

        private (double[] widths, double[] heights) GetCellWidthsAndHeights(int noOfRows, int noOfColumns)
        {
            // widths and heights represnt each size of cell in terms of with and hieght 
            var widths = new double[noOfColumns];
            var heights = new double[noOfRows + 1];

            // 100 is the width of each column
            widths = widths.Select(x => x = (double)100).ToArray();
            // 20 is the height of each column
            heights = heights.Select(x => x = (double)20).ToArray();

            return (widths, heights);
        }
    }
}
