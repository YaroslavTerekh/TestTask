using BlobTask.EmailFunction.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobTask.EmailFunction.Services.Abstractions;

public interface IEmailSender
{
    public void SendEmail(Message message, string name);
}
