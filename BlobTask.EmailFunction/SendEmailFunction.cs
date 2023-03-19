using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace BlobTask.EmailFunction
{
    [StorageAccount("BlobConnectionString")]
    public class SendEmailFunction
    {
        [FunctionName("SendEmailFunction")]
        public void Run(
            [BlobTrigger("testcontainer/{name}")] Stream myBlob, 
            string name,
            ILogger log
        )
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=blobstoragetesttask;AccountKey=HPD7XY0yhFiT3jnCwIzx+7UH3Yj3QIm0KU3L3lvWo56MAlEWXuiL+18jvXfreDgD6C/Jy0PGast9+AStS5RDAA==;EndpointSuffix=core.windows.net");
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("testcontainer");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            blockBlob.FetchAttributes();

            string emailAddress = blockBlob.Metadata["email"];
            if (string.IsNullOrEmpty(emailAddress))
            {
                log.LogWarning($"Email address is not provided for blob '{name}'. Email cannot be sent.");
                return;
            }

            var fromAddress = new MailAddress("yaroslav.terekh.reenbit@gmail.com", "Azure notification");
            var toAddress = new MailAddress(emailAddress, "Recipient");
            const string fromPassword = "esysvhbumcoapama";
            const string subject = "New blob uploaded";
            string body = $"<strong>A new file {name} has been successfully uploaded to the Azure Blob Storage!</strong>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
        }
    }
}