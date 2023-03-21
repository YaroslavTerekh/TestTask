using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobTask.EmailFunction.Services.Abstractions
{
    public interface IBlobSettings
    {
        public string GetEmailFromBlob(string fileName);

        public string CreateUriFromBlob(string fileName);
    }
}
