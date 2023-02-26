namespace FindPets.Services.Animals;

using AutoMapper;
using FindPets.Context;
using Microsoft.EntityFrameworkCore;

public class AnimalService : IAnimalService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;

    public AnimalService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<AnimalModel>> GetAnimals(int offset = 0, int limit = 10)
    {
        //using var context = await contextFactory.CreateDbContextAsync();

        //var Animals = context
        //    .Animals
        //    .AsQueryable();

        //var data = (await Animals.ToListAsync()).Select(Animal => mapper.Map<AnimalModel>(Animal));

        //return data;


        using var context = await contextFactory.CreateDbContextAsync();

        var animals = context
            .Animals
            .AsQueryable();

        animals = animals
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var data = (await animals.ToListAsync()).Select(animal => mapper.Map<AnimalModel>(animal));

        return data;


    }


    //public Task<IEnumerable<AnimalModel>> GetAnimals(int offset = 0, int limit = 10)
    //{
    //    using var context = await contextFactory.CreateDbContextAsync();

    //    var animals = context
    //        .Animals
    //        .AsQueryable();

    //    animals = animals
    //        .Skip(Math.Max(offset, 0))
    //        .Take(Math.Max(0, Math.Min(limit, 1000)));

    //    var data = (await animals.ToListAsync()).Select(animal => mapper.Map<AnimalModel>(animal));

    //    return data;
    //}


}