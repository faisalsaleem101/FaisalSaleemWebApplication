﻿using Aspose.Slides;
using DocumentGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Drawing;
using System.Data;

namespace DocumentGenerator.PowerPoint
{
    public class TableGeneratorPowerPoint
    {
        const string existingTableFile = "UpdateExistingTable";
        const int rowsPerSlide = 11;

        public string Run<T>(IEnumerable<T> data, string title) 
        {
            // The path to the documents directory.
            var templateDir = Helper.GetTemplatePath();
            var outputDir = Helper.GetOutputPath();

            // Retrieve the data from our data source which is stored as a DataTable.
            DataTable dataTable = Helper.CreateDataTable(data);

            // Instantiate Presentation class that represents PPTX// Instantiate Presentation class that represents PPTX
            using (Presentation pres = new Presentation($"{templateDir}//{existingTableFile}{dataTable.Columns.Count}.pptx"))
            {
                int noOfSlides = CreateNumberOfSlides(dataTable.Rows.Count, pres);

                int rowCounter = 0;
                int slide = 0;
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    // Access the slide
                    ISlide sld = pres.Slides[slide];

                    // get title object
                    var header = (IAutoShape)sld.Shapes.FirstOrDefault(c => c is IAutoShape);
                    header.TextFrame.Text = title;

                    // get table object 
                    var tbl = (ITable)sld.Shapes.FirstOrDefault(c => c is ITable);
                    List<string> columnNames = Helper.GetColumnNames(dataTable);

                    // if this is the first row create the blank column and rows for the slide so data can be inserted 
                    if (rowCounter == 0)
                        CreateEmptyColumnAndsRows(columnNames, tbl);

                    // assign data to the row fields
                    // start at the second row of the table since first is the colums
                    // use row varaible instead of rowcounter since it is not limted to e.g 10
                    for (int c = 0; c < dataTable.Columns.Count; c++)
                    {
                        tbl[c, rowCounter + 1].TextFrame.Text = dataTable.Rows[row][c]?.ToString() ?? "";

                        //change background colour of every odd row
                        if (row % 2 != 0)
                        {
                            tbl[c, rowCounter + 1].FillFormat.SolidFillColor.Color = Color.FromArgb(102, 102, 102);
                        }
                    }

                    // if the total number of rows perslide have been reached then set slide to next number and reset counter
                    if (rowCounter + 1 == rowsPerSlide)
                    {
                        slide++;
                        rowCounter = 0;
                    }
                    else
                    {
                        rowCounter++;
                    }
                }

                //Write the PPTX to Disk
                pres.Save($"{outputDir}//{title}.pptx", Aspose.Slides.Export.SaveFormat.Pptx);
            }

            return $"{title}.pptx";
        }

       
        private static void CreateEmptyColumnAndsRows(List<string> columnNames, ITable tbl)
        {
            // create number of columns 
            for (int i = 1; i < columnNames.Count; i++)
                tbl.Columns.AddClone(tbl.Columns[0], true);

            // assign column names 
            for (int i = 0; i < columnNames.Count; i++)
                tbl[i, 0].TextFrame.Text = columnNames[i];

            // create number of rows 
            for (int i = 1; i < rowsPerSlide; i++)
                tbl.Rows.AddClone(tbl.Rows[1], true);
        }

        private static int CreateNumberOfSlides(int totalRows, Presentation pres)
        {
            var noOfSlides = totalRows / rowsPerSlide;

            // if total rows are less than rowsPerSlide then set no of slides from 0 to 1 
            if (totalRows < rowsPerSlide)
                noOfSlides = 1;

            // Clone the desired slide from the source presentation to the end of the collection of slides in destination presentation
            ISlideCollection slideCollection = pres.Slides;

            // Clone the desired slide from the source presentation to the specified position in destination presentation
            for (int i = 1; i <= noOfSlides; i++)
                slideCollection.InsertClone(1, pres.Slides[0]);

            return noOfSlides;
        }
    }
}
