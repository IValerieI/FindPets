namespace FindPets.Services.Animals;

using AutoMapper;
using FindPets.Common.Validator;
using FindPets.Context;
using FindPets.Context.Entities;
using Microsoft.EntityFrameworkCore;


public class AnimalService : IAnimalService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddAnimalModel> addAnimalModelValidator;

    public AnimalService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddAnimalModel> addAnimalModelValidator
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.addAnimalModelValidator = addAnimalModelValidator;
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

        var book = await context.Animals.FirstOrDefaultAsync(x => x.Id.Equals(id));

        var data = mapper.Map<AnimalModel>(book);

        return data;
    }


    public async Task<AnimalModel> AddAnimal(AddAnimalModel model)
    {
        addAnimalModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var animal = mapper.Map<Animal>(model);
        await context.Animals.AddAsync(animal);
        context.SaveChanges();

        return mapper.Map<AnimalModel>(animal);
    }


}