using Azure.Storage.Blobs;
using BlobTask.Backend.Services.Abstractions;

namespace BlobTask.Backend.Services.Realizations
{
    public class BlobService : IBlobService
    {
        private readonly IConfiguration _configuration;

        public BlobService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task UploadBlobAsync(IFormFile file, Dictionary<string, string> metadata, CancellationToken cancellationToken = default)
        {
            var connectionString = _configuration.GetSection("AzureStorage").GetSection("ConnectionString").Value;
            var containerName = _configuration.GetSection("AzureStorage").GetSection("ContainerName").Value;
            var fileName = $"{DateTime.UtcNow.Millisecond}{new Random().Next(1, 10000)}{file.FileName}";

            var container = new BlobContainerClient(connectionString, containerName);
            var blob = container.GetBlobClient($"doc_{fileName}");

            using(var stream = file.OpenReadStream())
            {
                await blob.UploadAsync(stream);
                await blob.SetMetadataAsync(metadata);
            }
        }
    }
}
