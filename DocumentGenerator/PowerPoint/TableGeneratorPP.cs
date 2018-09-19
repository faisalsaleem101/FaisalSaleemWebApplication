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
        public void Run()
        {
            //ExStart:UpdateExistingTable
            // The path to the documents directory.

            var dataDir = Helper.GetOutputPath();

            //// Instantiate Presentation class that represents PPTX// Instantiate Presentation class that represents PPTX
            //using (Presentation pres = new Presentation(dataDir + "UpdateExistingTable.pptx"))
            //{

            //    // Access the first slide
            //    ISlide sld = pres.Slides[0];

            //    // Initialize null TableEx
            //    ITable tbl = null;

            //    // Iterate through the shapes and set a reference to the table found
            //    foreach (IShape shp in sld.Shapes)
            //        if (shp is ITable)
            //            tbl = (ITable)shp;

            //    // Set the text of the first column of second row
            //    tbl[0, 1].TextFrame.Text = "New";

            //    //Write the PPTX to Disk
            //    pres.Save(dataDir + "table1_out.pptx", Aspose.Slides.Export.SaveFormat.Pptx);
            //}
            //ExEnd:UpdateExistingTable
        }


       
    }
}
