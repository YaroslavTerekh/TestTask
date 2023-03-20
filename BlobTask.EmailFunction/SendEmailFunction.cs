using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using Azure.Storage.Sas;
using BlobTask.EmailFunction.Models;
using BlobTask.EmailFunction.Services.Abstractions;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BlobTask.EmailFunction
{
    [StorageAccount("BlobConnectionString")]
    public class SendEmailFunction
    {
        private readonly IEmailSender _emailSender;

        public SendEmailFunction(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        [FunctionName("SendEmailFunction")]
        public void Run(
            [BlobTrigger("testcontainer/{name}")] Stream myBlob, 
            string name,
            ILogger log
        )
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(Environment.GetEnvironmentVariable("BlobConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("testcontainer");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            blockBlob.FetchAttributes();

            string emailAddress = blockBlob.Metadata["email"];

            var blobServiceClient = new BlobServiceClient(Environment.GetEnvironmentVariable("BlobConnectionString"));
            var containerClient = blobServiceClient.GetBlobContainerClient("testcontainer");
            var blobClient1 = containerClient.GetBlockBlobClient(name);
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = "testcontainer",
                BlobName = name,
                Resource = "b",
                StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
                ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(5),
                Protocol = SasProtocol.Https,
            };
            sasBuilder.SetPermissions(BlobSasPermissions.Read);
            var url = blobClient1.GenerateSasUri(sasBuilder);

            var message = new Message(emailAddress, "Blob was uploaded", $"Your file <a href={url.OriginalString}>{name}</a> was uploaded");
            _emailSender.SendEmail(message, "Azure notification");
        }
    }
}