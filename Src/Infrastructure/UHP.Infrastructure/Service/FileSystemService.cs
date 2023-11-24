
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using UHP.Infrastructure.Helper;
using UHP.Infrastructure.Interfaces;

namespace UHP.Infrastructure.Service
{
    public class FileSystemService : IFileSystemService
    {
        private readonly IHostEnvironment _hostingEnvironment;

        public FileSystemService(IHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public string GetRootPath()
        {
            return Path.Combine(_hostingEnvironment.ContentRootPath, "wwwroot");
        }

        public string CreateDirectory(string path, string directory, string subdirectory, string filename)
        {
            var directoryPath = Path.Combine(path, directory, subdirectory);

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            return Path.Combine(directoryPath, filename);
        }

        public async Task<string> SaveFile(string directory, string subdirectory, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return null;
                }

                var filename = file.GetFilenameWithoutExtension() + "_" + Guid.NewGuid() + file.GetFileExtension();

                var path = CreateDirectory(GetRootPath(), directory, subdirectory, filename);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Position = 0;
                }

                return Path.Combine(GetRootPath(), directory, subdirectory, filename);
            }
            catch (Exception exception)
            {
                Console.WriteLine("Save File Error: ");
                Console.WriteLine(exception.Message);
                return null;
            }
        }
        
        public void DeleteFile(string filename)
        {
            try
            {
                if (string.IsNullOrEmpty(filename))
                {
                    File.Delete(Path.Combine(GetRootPath(), filename));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Delete File Error: ");
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteFiles(List<string> filenames)
        {
            filenames.ForEach(s =>
            {
                if (!string.IsNullOrEmpty(s))
                {
                    File.Delete(Path.Combine(GetRootPath(), s));
                }
            });
        }
    }
}