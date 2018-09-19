using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentGenerator.Helpers
{
    public class Helper
    {
        const string outputFolder = "Outputs";

        public static string GetOutputPath()
        {
            string dataDir = Directory.GetCurrentDirectory();
            var startDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(dataDir).FullName).FullName).FullName).FullName;
            string outputPath = Path.Combine(startDirectory, outputFolder);

            return outputPath;
        }
    }
}
