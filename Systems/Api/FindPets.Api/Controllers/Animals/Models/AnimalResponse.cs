namespace FindPets.API.Controllers.Animals.Models;


using AutoMapper;
using FindPets.Context.Entities;
using FindPets.Services.Animals;

public class AnimalResponse
{
    public int Id { get; set; }

    public string Kind { get; set; }
    public string Breed { get; set; }

    public string Description { get; set; }
    public string Image { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
}

public class AnimalResponseProfile : Profile
{
    public AnimalResponseProfile()
    {
        CreateMap<AnimalModel, AnimalResponse>();
    }
}