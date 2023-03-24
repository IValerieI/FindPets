namespace FindPets.Services.Animals;

using AutoMapper;
using FindPets.Common.Exceptions;
using FindPets.Common.Validator;
using FindPets.Context;
using FindPets.Context.Entities;
using FindPets.Services.EmailSender;
using Microsoft.EntityFrameworkCore;

public class AnimalService : IAnimalService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddAnimalModel> addAnimalModelValidator;
    private readonly IModelValidator<UpdateAnimalModel> updateAnimalModelValidator;
    private readonly IEmailSenderService emailSender;

    public AnimalService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddAnimalModel> addAnimalModelValidator,
        IModelValidator<UpdateAnimalModel> updateAnimalModelValidator,
        IEmailSenderService emailSender
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.addAnimalModelValidator = addAnimalModelValidator;
        this.updateAnimalModelValidator = updateAnimalModelValidator;
        this.emailSender = emailSender;
    }

    public AnimalService() { }

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


    public async Task<AnimalModel> AddAnimal(AddAnimalModel model)
    {
        addAnimalModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var animal = mapper.Map<Animal>(model);
        await context.Animals.AddAsync(animal);
        context.SaveChangesAsync();

        SendEmail(model);

        return mapper.Map<AnimalModel>(animal);
    }

    private void SendEmail(AddAnimalModel model)
    {
        EmailModel emailModel = new EmailModel();
        emailModel.Subject = "New lost animal";
        emailModel.Message = "New lost aimal is a " + model.Kind + " and " + model.Breed;

        emailSender.SendEmail(emailModel);
    }






    public async Task UpdateAnimal(int animalId, UpdateAnimalModel model)
    {
        updateAnimalModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Id.Equals(animalId));

        ProcessException.ThrowIf(() => animal is null, $"The animal (id: {animalId}) was not found");

        animal = mapper.Map(model, animal);

        context.Animals.Update(animal);
        context.SaveChangesAsync();
    }


    public async Task DeleteAnimal(int animalId)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var animal = await context.Animals.FirstOrDefaultAsync(x => x.Id.Equals(animalId))
            ?? throw new ProcessException($"The animal (id: {animalId}) was not found");

        context.Remove(animal);
        context.SaveChangesAsync();
    }


}