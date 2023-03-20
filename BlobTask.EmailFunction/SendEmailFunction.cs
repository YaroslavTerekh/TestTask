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
        private readonly IBlobSettings _blobSettings;

        public SendEmailFunction(IEmailSender emailSender, IBlobSettings blobSettings)
        {
            _emailSender = emailSender;
            _blobSettings = blobSettings;
        }

        [FunctionName("SendEmailFunction")]
        public void Run(
            [BlobTrigger("testcontainer/{name}")] Stream myBlob, 
            string name,
            ILogger log
        )
        {
            var message = new Message(_blobSettings.GetEmailFromBlob(name), "Blob was uploaded", $"Your file <a href={_blobSettings.CreateUriFromBlob(name)}>{name}</a> was uploaded");
            _emailSender.SendEmail(message, "Azure notification");
        }
    }
}