using AutoMapper;
using FindPets.Context.Entities;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace FindPets.Services.Requests;

public class AddRequestModel
{
    public int AnimalId { get; set; }

    public string Name { get; set; } = string.Empty;
    [Phone]
    public string Phone { get; set; } = string.Empty;


}

public class AddRequestModelValidator : AbstractValidator<AddRequestModel>
{
    public AddRequestModelValidator()
    {
        RuleFor(x => x.AnimalId)
            .NotEmpty().WithMessage("AnimalId is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name is too long.");

        // add proper validation for phone number
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required.");


    }
}

public class AddRequestModelProfile : Profile
{
    public AddRequestModelProfile()
    {
        CreateMap<AddRequestModel, Request>();
    }
}