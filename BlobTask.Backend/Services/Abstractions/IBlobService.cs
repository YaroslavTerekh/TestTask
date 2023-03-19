namespace BlobTask.Backend.Services.Abstractions
{
    public interface IBlobService
    {
        public Task UploadBlobAsync(IFormFile file, Dictionary<string, string> metadata, CancellationToken cancellationToken = default);
    }
}
