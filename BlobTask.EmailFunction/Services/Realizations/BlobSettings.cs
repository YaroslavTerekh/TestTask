using System;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using BlobTask.EmailFunction.Services.Abstractions;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace BlobTask.EmailFunction.Services.Realizations
{
    public class BlobSettings : IBlobSettings
    {
        public string GetEmailFromBlob(string fileName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("BlobConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(Environment.GetEnvironmentVariable("ContainerName"));
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            blockBlob.FetchAttributes();

            string emailAddress = blockBlob.Metadata["email"];

            return emailAddress;
        }

        public string CreateUriFromBlob(string fileName)
        {
            var containerName = Environment.GetEnvironmentVariable("ContainerName");
            var blobServiceClient = new BlobServiceClient(Environment.GetEnvironmentVariable("BlobConnectionString"));
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient1 = containerClient.GetBlockBlobClient(fileName);
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = containerName,
                BlobName = fileName,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5),
                Protocol = SasProtocol.Https,
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);
            var url = blobClient1.GenerateSasUri(sasBuilder);

            return url.OriginalString;
        }
    }
}
