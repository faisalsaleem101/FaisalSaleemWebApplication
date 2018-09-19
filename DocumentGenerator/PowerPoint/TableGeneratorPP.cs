using Aspose.Slides;
using DocumentGenerator.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentGenerator.PowerPoint
{
    public class TableGeneratorPP
    {
        const string existingTableFile = "UpdateExistingTable.pptx";
        const string outputTableFile = "OutputTable.pptx";

        public void Run(List<string> columns, string[,] data) 
        {

            // The path to the documents directory.
            var templateDir = Helper.GetTemplatePath();
            var outputDir = Helper.GetOutputPath();

            // Instantiate Presentation class that represents PPTX// Instantiate Presentation class that represents PPTX
            using (Presentation pres = new Presentation( $"{templateDir}//{existingTableFile}"))
            {
                // Access the first slide
                ISlide sld = pres.Slides[0];

                // Initialize null TableEx
                ITable tbl = null;

                // Iterate through the shapes and set a reference to the table found
                foreach (IShape shp in sld.Shapes)
                    if (shp is ITable)
                        tbl = (ITable)shp;

                // create number of columns 
                for (int i = 1; i < columns.Count; i++)
                    tbl.Columns.AddClone(tbl.Columns[0], true);

                // assign column names 
                for (int i = 0; i < columns.Count; i++)
                    tbl[i, 0].TextFrame.Text = columns[i];

                // create n,umber of rows 
                for (int i = 1; i < data.GetUpperBound(0); i++)
                    tbl.Rows.AddClone(tbl.Rows[1], true);

                //Write the PPTX to Disk
                pres.Save($"{outputDir}//{outputTableFile}", Aspose.Slides.Export.SaveFormat.Pptx);
            }
        }


       
    }
}
