using BlobTask.Backend.Behaviors.Uploads.UploadFile;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlobTask.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadsController : ControllerBase
    {
        private readonly IMediator _mediatr;
        private readonly IValidator<UploadFileCommand> _uploadFileCommandValidator;

        public UploadsController(
            IMediator mediatr, 
            IValidator<UploadFileCommand> uploadFileCommandValidator
        )
        {
            _mediatr = mediatr;
            _uploadFileCommandValidator = uploadFileCommandValidator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFileAsync(
            [FromForm] UploadFileCommand command,
            CancellationToken cancellationToken = default
        )
        {
            await _uploadFileCommandValidator.ValidateAsync(command, cancellationToken);
            await _mediatr.Send(command, cancellationToken);

            return Ok();
        }
    }
}
