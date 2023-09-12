using BlobTask.EmailFunction;
using BlobTask.EmailFunction.Models;
using BlobTask.EmailFunction.Services.Abstractions;
using BlobTask.EmailFunction.Services.Realizations;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(Startup))]
namespace BlobTask.EmailFunction;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var emailConfig = new EmailConfiguration
        { 
            From = Environment.GetEnvironmentVariable("From"),
            SmtpServer = "smtp.gmail.com",
            Port = 465,
            UserName = "slavik.terekh@gmail.com",
            Password = Environment.GetEnvironmentVariable("Password")
        };

        builder.Services.AddSingleton(emailConfig);
        builder.Services.AddScoped<IEmailSender, EmailSender>();
        builder.Services.AddTransient<IBlobSettings, BlobSettings>();
    }
}
