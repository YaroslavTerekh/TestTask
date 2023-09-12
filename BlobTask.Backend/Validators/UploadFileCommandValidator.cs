using BlobTask.Backend.Behaviors.Uploads.UploadFile;
using FluentValidation;

namespace BlobTask.Backend.Validators;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(t => t.Email)
            .NotNull()
            .WithMessage("Email is required")
            .NotEmpty()
            .WithMessage("Email is required")
            .NotEqual(String.Empty)
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Email is incorrect");

        RuleFor(t => t.File)
            .NotNull()
            .WithMessage("File is required")
            .NotEmpty()
            .WithMessage("File is required")
            .Must(t => Path.GetExtension(t.FileName) == ".docx")
            .WithMessage("Only files with .docx extension allowed");
    }
}
