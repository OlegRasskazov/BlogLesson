using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Blog.Data.FileManager
{
    public interface IFileManager
    {
        Task<string> SaveImage(IFormFile image);

        FileStream ImageStream(string str);
    }
}
