using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceAPI.Infrastructure.Services
{
    public class FileService 
    {
        public async Task<string> FileRenameAsync(string path, string fileName)
        {
            string extension = Path.GetExtension(fileName);
            string oldName = Path.GetFileNameWithoutExtension(fileName);

            string regulatedName = $"{NameOperation.NameRegulation(oldName)}{extension}";
            int i = 2;

            while (File.Exists(Path.Combine(path, regulatedName)))
            {
                regulatedName = $"{NameOperation.NameRegulation(oldName)}-{i}{extension}";
                i++;
            }

            return await Task.FromResult(regulatedName);
        }





    }
}
