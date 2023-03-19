using MediatR;

namespace BlobTask.Backend.Behaviors.Uploads.UploadFile
{
    public class UploadFileCommand : IRequest
    {
        public string Email { get; set; }

        public IFormFile File { get; set; }

        public UploadFileCommand()
        {

        }
    }
}
