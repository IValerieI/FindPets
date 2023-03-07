namespace FindPets.Services.Animals;

using AutoMapper;
using FindPets.Common.Exceptions;
using FindPets.Common.Validator;
using FindPets.Context;
using FindPets.Context.Entities;
using Microsoft.EntityFrameworkCore;

public class AnimalService : IAnimalService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddAnimalModel> addAnimalModelValidator;
    private readonly IModelValidator<UpdateAnimalModel> updateAnimalModelValidator;

    public AnimalService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddAnimalModel> addAnimalModelValidator,
        IModelValidator<UpdateAnimalModel> updateAnimalModelValidator
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.addAnimalModelValidator = addAnimalModelValidator;
        this.updateAnimalModelValidator = updateAnimalModelValidator;
    }

    public async Task<IEnumerable<AnimalModel>> GetAnimals(int offset = 0, int limit = 10)
    {
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


    public async Task<AnimalModel> GetAnimal(int id)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var animal = await context
            .Animals
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id.Equals(id));

        var data = mapper.Map<AnimalModel>(animal);

        return data;
    }

    //public async Task<AnimalModel> GetAnimalWithComments(int id)
    //{
    //    using var context = await contextFactory.CreateDbContextAsync();

    //    var animalAndComments = await context
    //        .Animals
    //        .Include(x => x.Comments)
    //        .FirstOrDefaultAsync(x => x.Id.Equals(id));

    //    var data = mapper.Map<AnimalModel>(animalAndComments);

    //    return data;
    //}


    public async Task<AnimalModel> AddAnimal(AddAnimalModel model)
    {
        addAnimalModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();




        var animal = mapper.Map<Animal>(model);
        await context.Animals.AddAsync(animal);
        context.SaveChanges();

        return mapper.Map<AnimalModel>(animal);
    }


    public async Task UpdateAnimal(int animalId, UpdateAnimalModel model)
    {
        updateAnimalModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Id.Equals(animalId));

        ProcessException.ThrowIf(() => animal is null, $"The animal (id: {animalId}) was not found");

        animal = mapper.Map(model, animal);

        context.Animals.Update(animal);
        context.SaveChanges();
    }


    public async Task DeleteAnimal(int animalId)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Id.Equals(animalId))
            ?? throw new ProcessException($"The animal (id: {animalId}) was not found");

        context.Remove(animal);
        context.SaveChanges();
    }


}