using BlobTask.Backend.Behaviors.Uploads.UploadFile;
using BlobTask.Backend.Tests.common.constants;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlobTask.Backend.Tests.common.builders;

public static class MockData
{
    public static UploadFileCommand UploadFileCommand_MockData()
    {
        var fileMock = new Mock<IFormFile>();
        var physicalFile = new FileInfo(DataConstants.FilePath);
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(physicalFile.OpenRead());
        writer.Flush();
        ms.Position = 0;
        var fileName = physicalFile.Name;

        fileMock.Setup(_ => _.FileName).Returns(fileName);
        fileMock.Setup(_ => _.Length).Returns(ms.Length);
        fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
        fileMock.Setup(_ => _.ContentDisposition).Returns(string.Format("inline; filename={0}", fileName));

        return new UploadFileCommand()
        {
            Email = DataConstants.Email,
            File = fileMock.Object,
        };
    }
}
