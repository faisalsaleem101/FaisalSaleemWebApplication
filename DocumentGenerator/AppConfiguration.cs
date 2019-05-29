using Microsoft.Extensions.Configuration;
using System.IO;

namespace DocumentGenerator
{
    public class AppConfiguration
    {
        readonly string outputFolderDirectory = string.Empty;

        public AppConfiguration()
        {
            string path = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            outputFolderDirectory = Path.GetFullPath(Path.Combine(path, @"..\", configuration["Config:OutputFolder"]));

        }
        public string OutputFolderDirectory
        {
            get => outputFolderDirectory;
        }
    }
}
