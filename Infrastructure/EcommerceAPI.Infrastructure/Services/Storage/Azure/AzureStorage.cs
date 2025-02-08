using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EcommerceAPI.Application.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace EcommerceAPI.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage :Storage, IAzureStorage
    { 

         readonly BlobServiceClient _blobServiceClient;
         BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }

        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(f => f.Name).ToList();
            
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(f => f.Name == fileName);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient =  _blobServiceClient.GetBlobContainerClient(containerName);//which container
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string pathOrContainerName)> datas = new();
            foreach (IFormFile file in files)
            {
               string regulatedName = await FileRenameAsync(containerName, file.Name, HasFile);
               BlobClient blobClient = _blobContainerClient.GetBlobClient(regulatedName);
               await blobClient.UploadAsync(file.OpenReadStream());
               datas.Add((regulatedName, $"{containerName}/{regulatedName}"));

            }
            return datas;
        }
    }
}
