using BlobTask.Backend.Services.Abstractions;
using MediatR;

namespace BlobTask.Backend.Behaviors.Uploads.UploadFile
{
    public class UploadFileHandler : IRequestHandler<UploadFileCommand>
    {
        private readonly IBlobService _blobService;

        public UploadFileHandler(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task Handle(UploadFileCommand request, CancellationToken cancellationToken = default)
        {
            var metadata = new Dictionary<string, string>{
                {"email", request.Email}
            };

            await _blobService.UploadBlobAsync(request.File, metadata, cancellationToken);
        }
    }
}
