using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UHP.Infrastructure.Helper
{
    public static class FormFileExtensions
    {
        public static string GetFilename(this IFormFile file)
        {
            return ContentDispositionHeaderValue.Parse(
                file.ContentDisposition).FileName.Trim('"');
        }

        public static string GetFilenameWithoutExtension(this IFormFile file)
        {
            return Path.GetFileNameWithoutExtension(GetFilename(file));
        }

        public static string GetFileExtension(this IFormFile file)
        {
            return Path.GetExtension(GetFilename(file));
        }

        public static async Task<MemoryStream> GetFileStream(this IFormFile file)
        {
            var filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream;
        }

        public static async Task<byte[]> GetFileArray(this IFormFile file)
        {
            var filestream = new MemoryStream();
            await file.CopyToAsync(filestream);
            return filestream.ToArray();
        }

        public static byte[] GetFileArray(string filePath)
        {
            var stream = System.IO.File.OpenRead(filePath);
            var fileBytes = new byte[stream.Length];

            stream.Read(fileBytes, 0, fileBytes.Length);
            stream.Position = 0;
            stream.Close();

            return fileBytes;
        }

        public static string GetFileSize(this IFormFile file)
        {
            var source = file.Length;
            const int byteConversion = 1024;
            var bytes = Convert.ToDouble(source);
            if (bytes >= Math.Pow(byteConversion, 3)) //GB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 3), 2), " GB");
            }

            if (bytes >= Math.Pow(byteConversion, 2)) //MB Range
            {
                return string.Concat(Math.Round(bytes / Math.Pow(byteConversion, 2), 2), " MB");
            }

            if (bytes >= byteConversion) //KB Range
            {
                return string.Concat(Math.Round(bytes / byteConversion, 2), " KB");
            }

            //Bytes
            {
                return string.Concat(bytes, " Bytes");
            }
        }
    }
}