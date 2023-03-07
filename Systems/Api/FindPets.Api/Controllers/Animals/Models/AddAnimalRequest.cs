namespace FindPets.API.Controllers.Models;

using AutoMapper;
using FindPets.Services.Animals;
using FluentValidation;

public class AddAnimalRequest
{
    public string Kind { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } //= string.Empty;

    public IFormFile File { get; set; }


}

public class AddAnimalResponseValidator : AbstractValidator<AddAnimalRequest>
{
    public AddAnimalResponseValidator()
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

public class AddAnimalRequestProfile : Profile
{
    public AddAnimalRequestProfile()
    {
        CreateMap<AddAnimalRequest, AddAnimalModel>();
        //.ForMember(i => i.Image, a => a.MapFrom(f => f.File));
    }
}