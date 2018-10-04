using Aspose.Slides;
using DocumentGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace DocumentGenerator.PowerPoint
{
    public class TableGeneratorPP
    {
        const string existingTableFile = "UpdateExistingTable.pptx";
        const string outputTableFile = "OutputTable.pptx";

        const int rowsPerSlide = 10;

        public void Run (List<string> columns, string[,] data, string title) 
        {

            // The path to the documents directory.
            var templateDir = Helper.GetTemplatePath();
            var outputDir = Helper.GetOutputPath();

            // Instantiate Presentation class that represents PPTX// Instantiate Presentation class that represents PPTX
            using (Presentation pres = new Presentation( $"{templateDir}//{existingTableFile}"))
            {
                int noOfSlides = CreateNumberOfSlides(data, pres);

                // Access the first slide
                ISlide sld = pres.Slides[0];

                // get title object
                var header = (IAutoShape)sld.Shapes.FirstOrDefault(c => c is IAutoShape);
                header.TextFrame.Text = title;

                // get table object 
                var tbl = (ITable)sld.Shapes.FirstOrDefault(c => c is ITable);

                // create number of columns 
                for (int i = 1; i < columns.Count; i++)
                    tbl.Columns.AddClone(tbl.Columns[0], true);

                // assign column names 
                for (int i = 0; i < columns.Count; i++)
                    tbl[i, 0].TextFrame.Text = columns[i];


                // create number of rows 
                for (int i = 1; i < data.GetUpperBound(0); i++)
                    tbl.Rows.AddClone(tbl.Rows[1], true);

                //assign data to rows
                for (int r = 1; r <= data.GetUpperBound(0); r++)
                {
                    for (int c = 0; c <= data.GetUpperBound(1); c++)
                    {
                        tbl[c, r].TextFrame.Text = data[r - 1, c] ?? "";
                    }
                }


                //Write the PPTX to Disk
                pres.Save($"{outputDir}//{outputTableFile}", Aspose.Slides.Export.SaveFormat.Pptx);
            }
        }

        private static int CreateNumberOfSlides(string[,] data, Presentation pres)
        {
            var totalRows = data.GetUpperBound(0);
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
