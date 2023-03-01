namespace FindPets.Services.Animals;

using AutoMapper;
using FindPets.Context.Entities;

public class AnimalModel
{
    public int Id { get; set; }

    public string Kind { get; set; } = string.Empty;
    public string Breed { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
}

public class AnimalModelProfile : Profile
{
    public AnimalModelProfile()
    {
        CreateMap<Animal, AnimalModel>();
    }
}
