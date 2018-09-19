using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DocumentGenerator.Helpers
{
    public class Helper
    {
        const string outputFolder = "Outputs";
        const string templateFolder = "Templates";

        public static string GetOutputPath()
        {
            string startDirectory = GetRootDirectory();
            return Path.Combine(startDirectory, outputFolder);
        }

        public static string GetTemplatePath()
        {
            string startDirectory = GetRootDirectory();
            return Path.Combine(startDirectory, templateFolder);
        }

        private static string GetRootDirectory()
        {
            string dataDir = Directory.GetCurrentDirectory();
            return Directory.GetParent(dataDir).FullName;
        }
    }
}
