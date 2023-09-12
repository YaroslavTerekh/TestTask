using BlobTask.Backend.Services.Abstractions;
using MediatR;

namespace BlobTask.Backend.Behaviors.Uploads.UploadFile
{
    public class UploadFileHandler : IRequestHandler<UploadFileCommand, Dictionary<string, string>>
    {
        private readonly IBlobService _blobService;

        public UploadFileHandler(IBlobService blobService)
        {
            _blobService = blobService;
        }

        public async Task<Dictionary<string, string>> Handle(UploadFileCommand request, CancellationToken cancellationToken = default)
        {
            var metadata = new Dictionary<string, string>{
                {"email", request.Email}
            };

            await _blobService.UploadBlobAsync(request.File, metadata, cancellationToken);

            return metadata;
        }
    }
}
