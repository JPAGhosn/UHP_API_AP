using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UHP.Infrastructure.Interfaces
{
    public interface IFileSystemService
    {
        string GetRootPath();
        string CreateDirectory(string path, string directory, string subdirectory, string filename);
        Task<string> SaveFile(string directory, string subdirectory, IFormFile file);
        void DeleteFile(string filename);
    }
}