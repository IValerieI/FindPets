namespace FindPets.Services.Animals;

using AutoMapper;
using FindPets.Context.Entities;
using FluentValidation;

public class AddAnimalModel
{
    public string Kind { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}

public class AddAnimalModelValidator : AbstractValidator<AddAnimalModel>
{
    public AddAnimalModelValidator()
    {
        RuleFor(x => x.Kind)
            .NotEmpty().WithMessage("Kind is required.")
            .MaximumLength(50).WithMessage("Kind is too long.");

        RuleFor(x => x.Breed)
            .NotEmpty().WithMessage("Breed is required.")
            .MaximumLength(50).WithMessage("Breed is too long.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(2000).WithMessage("Description is too long.");

        RuleFor(x => x.Image)
            .NotEmpty().WithMessage("Image is required.");

    }
}

public class AddAnimalModelProfile : Profile
{
    public AddAnimalModelProfile()
    {
        CreateMap<AddAnimalModel, Animal>();
    }
}