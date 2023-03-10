using AutoMapper;
using FindPets.Services.Comments;
using FluentValidation;

namespace FindPets.API.Controllers.Models;

public class AddCommentRequest
{
    public int AnimalId { get; set; }

    //public string Animal { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;

    //public DateTime Created { get; set; } = DateTime.UtcNow;


}

public class AddCommentResponseValidator : AbstractValidator<AddCommentRequest>
{
    public AddCommentResponseValidator()
    {
        RuleFor(x => x.AnimalId)
            .NotEmpty().WithMessage("AnimalId is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name is too long.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Text is required.")
            .MaximumLength(2000).WithMessage("Text is too long.");

    }
}

public class AddCommentRequestProfile : Profile
{
    public AddCommentRequestProfile()
    {
        CreateMap<AddCommentRequest, AddCommentModel>();
    }
}