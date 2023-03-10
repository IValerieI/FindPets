using AutoMapper;
using FindPets.Context.Entities;
using FluentValidation;

namespace FindPets.Services.Comments;

public class AddCommentModel
{
    public int? AnimalId { get; set; }

    public string Name { get; set; }
    public string Text { get; set; }

    //public DateTime Created { get; set; } = DateTime.UtcNow;
}

public class AddCommentModelValidator : AbstractValidator<AddCommentModel>
{
    public AddCommentModelValidator()
    {
        RuleFor(x => x.AnimalId)
            .NotEmpty().WithMessage("AnimalId is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name is too long.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Text is required.");


    }
}

public class AddCommentModelProfile : Profile
{
    public AddCommentModelProfile()
    {
        CreateMap<AddCommentModel, Comment>();
    }
}