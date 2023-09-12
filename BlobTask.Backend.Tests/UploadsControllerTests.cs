using BlobTask.Backend.Behaviors.Uploads.UploadFile;
using BlobTask.Backend.Services.Realizations;
using BlobTask.Backend.Tests.common.builders;
using BlobTask.Backend.Tests.common.constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BlobTask.Backend.Tests;

public class UploadsControllerTests
{
    [Fact]
    public async Task UploadFileAsync_ReturnsExpectedMetadata()
    {
        var handler = new UploadFileHandler(new BlobService(CustomConfigurationBuilder.BuildConfiguration()));
        var expected = new Dictionary<string, string>();
        expected.Add("email", DataConstants.Email);

        var result = await handler.Handle(MockData.UploadFileCommand_MockData(), CancellationToken.None);

        Assert.Equal(expected, result);
    }

    [Fact]
    public async Task UploadFileAsync_ReturnsMetadata()
    {
        var handler = new UploadFileHandler(new BlobService(CustomConfigurationBuilder.BuildConfiguration()));

        var result = await handler.Handle(MockData.UploadFileCommand_MockData(), CancellationToken.None);

        Assert.NotNull(result);
    }
}