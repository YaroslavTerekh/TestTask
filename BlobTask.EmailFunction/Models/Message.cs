using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobTask.EmailFunction.Models;

public class Message
{
    public MailboxAddress To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public Message(string to, string subject, string content)
    {
        To = new MailboxAddress("Azure notification", to);
        Subject = subject;
        Content = content;
    }
}
