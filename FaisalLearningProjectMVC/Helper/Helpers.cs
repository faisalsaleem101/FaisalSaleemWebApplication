using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.Helper
{
    public static class Helpers
    {
        public static async Task<byte[]> DownloadFile(string fileName, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            var outputPath = GetOutputFolderPath(configuration, hostingEnvironment);

            var filePath = $"{outputPath}\\{fileName}";
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return fileBytes;
        }


        public static string GetOutputFolderPath(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            var directory = Directory.GetParent(hostingEnvironment.ContentRootPath).FullName;
            var outputFolder = configuration.GetValue<string>("Config:OutputFolder");

            return $"{directory}\\{outputFolder}";

        }

        public static string GetWordDocumentFileName(string name) => $"{name} {DateTime.Now.ToString().Replace("/", "_").Replace(":", "_")}.docx";

        public static string GetExcelDocumentFileName(string name) => $"{name} {DateTime.Now.ToString().Replace("/", "_").Replace(":", "_")}.xlsx";
    }
}
