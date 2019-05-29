using Microsoft.Extensions.Configuration;
using System.IO;

namespace DocumentGenerator
{
    public class AppConfiguration
    {
        public string OutputFolderDirectory { get; }


        public AppConfiguration()
        {
            string path = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder().SetBasePath(path).AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            OutputFolderDirectory = Path.GetFullPath(Path.Combine(path, @"..\", configuration["Config:OutputFolder"]));

        }

    }
}
