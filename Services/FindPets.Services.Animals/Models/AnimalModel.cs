namespace FindPets.Services.Animals;

using AutoMapper;
using FindPets.Context.Entities;

public class AnimalModel
{
    public int Id { get; set; }

    public string Kind { get; set; }
    public string Breed { get; set; }

    public string Description { get; set; }
    public string Image { get; set; }
}

public class AnimalModelProfile : Profile
{
    public AnimalModelProfile()
    {
        CreateMap<Animal, AnimalModel>();
    }
}
