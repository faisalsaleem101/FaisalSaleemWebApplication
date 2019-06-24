using Spire.Presentation;
using System.Collections.Generic;

namespace DocumentGenerator.PowerPoint
{
    public class TableGeneratorPowerPoint
    {
        const string existingTableFile = "UpdateExistingTable";
        const int rowsPerSlide = 11;

        public void Run<T>(IEnumerable<T> data, string title)
        {

            string filePath = @"C:\Users\Faisal Saleem\source\repos\FaisalLearningProjectMVC\Templates\UpdateExistingTable6.pptx";

            Presentation presentation = new Presentation();

            double[] widths = new double[] { 100, 100, 150, 100, 100 };

            double[] heights = new double[] { 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15 };

            ITable table = presentation.Slides[0].Shapes.AppendTable(presentation.SlideSize.Size.Width / 2 - 275, 80, widths, heights);

            presentation.SaveToFile(filePath, FileFormat.Pptx2010);



        }

    }
}
