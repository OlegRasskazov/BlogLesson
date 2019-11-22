using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Blog.Data.FileManager
{
    public class FileManager: IFileManager
    {
        private string _imagePath;

        public FileManager(IConfiguration config)
        {
            _imagePath = config["Path:Images"];
        }

        public async Task<string> SaveImage(IFormFile image)
        {
            try
            {


            var savePath = Path.Combine(_imagePath);
            if (!Directory.Exists(_imagePath))
            {
                Directory.CreateDirectory(savePath);
            }

            var mime = image.FileName.Substring(image.FileName.LastIndexOf('.'));
            var fileName = $"img_{DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}{mime}";

            using (var fileStream = new FileStream(Path.Combine(savePath, fileName), FileMode.Create))
            {
               await image.CopyToAsync(fileStream);
            }

            return fileName;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "Error";
            }
        }

        public FileStream ImageStream(string str)
        {
            return new FileStream(Path.Combine(_imagePath,str),FileMode.Open ,FileAccess.Read);
        }
    }
}
