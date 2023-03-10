namespace FindPets.API.Controllers.Models;

using AutoMapper;
using FindPets.Services.Requests;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

public class AddRequestRequest
{
    public int AnimalId { get; set; }

    public string Name { get; set; } = string.Empty;
    [Phone]
    public string Phone { get; set; } = string.Empty;

}


public class AddRequestResponseValidator : AbstractValidator<AddRequestRequest>
{
    public AddRequestResponseValidator()
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


public class AddRequestRequestProfile : Profile
{
    public AddRequestRequestProfile()
    {
        CreateMap<AddRequestRequest, AddRequestModel>();
    }
}