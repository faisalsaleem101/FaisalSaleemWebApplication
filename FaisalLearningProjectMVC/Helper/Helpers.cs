﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace FaisalLearningProjectMVC.Helper
{
    public static class Helpers
    {
        public static async Task<byte[]> DownloadFile(string fileName, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            var directory = Directory.GetParent(hostingEnvironment.ContentRootPath).FullName;
            var outputFolder = configuration.GetValue<string>("MyConfig:OutputFolder");

            var filePath = $"{directory}\\{outputFolder}\\{fileName}";
            byte[] fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);

            return fileBytes;
        }
    }
}