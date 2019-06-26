using DocumentGenerator.Helpers;
using Spire.Presentation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DocumentGenerator.PowerPoint
{
    public class TableGeneratorPowerPoint
    {
        const int rowsPerSlide = 11;

        public void Run<T>(IEnumerable<T> data, string title)
        {

            string filePath = @"C:\Users\Faisal Saleem\source\repos\FaisalLearningProjectMVC\Templates\UpdateExistingTable7.pptx";

            Presentation presentation = new Presentation();

            int totalNoOfRows = data.ToList().Count();
            int noOfSlides = totalNoOfRows / rowsPerSlide;

            // use reflection to get the number of columns in the list
            int noOfColumns = data.FirstOrDefault()?.GetType()?.GetProperties()?.Length ?? 0;
            // use reflection to get the column names
            var columnNames = data.FirstOrDefault()?.GetType()?.GetProperties()?.Select(c => c.Name).ToList();
            // add space between capital letters
            columnNames = columnNames.Select(c => (string.Concat(c.Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' '))).ToList();


            // create no of slides needed is spire
            for (int i = 0; i < noOfSlides; i++)
                presentation.Slides.Append();

            var widthsAndHeights = GetCellWidthsAndHeights(totalNoOfRows, noOfColumns);

            // create table
            ITable table = presentation.Slides[0].Shapes.AppendTable(presentation.SlideSize.Size.Width / 2 - 275, 80, widthsAndHeights.widths, widthsAndHeights.heights);


            // insert column names on first row
            for (int c = 0; c < columnNames.Count; c++)
            {
                table[c, 0].TextFrame.Text = columnNames[c];
                table[c, 0].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Calibri");
                table[c, 0].TextFrame.Paragraphs[0].TextRanges[0].FontHeight = 12;
            }

            DataTable dataTable = Helper.CreateDataTable(data);

            // insert data
            for (int r = 0; r < dataTable.Rows.Count; r++)
            {
                for (int c = 0; c < dataTable.Columns.Count; c++)
                {
                    table[c, r + 1].TextFrame.Text = dataTable.Rows[r][c]?.ToString() ?? "";
                    table[c, r + 1].TextFrame.Paragraphs[0].TextRanges[0].LatinFont = new TextFont("Calibri");
                    table[c, r + 1].TextFrame.Paragraphs[0].TextRanges[0].FontHeight = 12;

                }
            }

            // save file
            presentation.SaveToFile(filePath, FileFormat.Pptx2010);



        }

        private (double[] widths, double[] heights) GetCellWidthsAndHeights(int totalNoOfRows, int noOfColumns)
        {
            // widths and heights represnt each size of cell in terms of with and hieght 
            // create widths/heights in double array since double array is what spire uses to create a table
            var widths = new double[noOfColumns];
            var heights = new double[totalNoOfRows + 1];


            // 100 is the width of each column
            widths = widths.Select(x => x = (double)100).ToArray();
            // 20 is the height of each column
            heights = heights.Select(x => x = (double)20).ToArray();

            return (widths, heights);
        }
    }
}
