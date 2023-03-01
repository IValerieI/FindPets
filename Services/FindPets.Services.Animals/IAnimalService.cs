namespace FindPets.Services.Animals;

public interface IAnimalService
{
    Task<IEnumerable<AnimalModel>> GetAnimals(int offset = 0, int limit = 10);
    Task<AnimalModel> GetAnimal(int animalId);
    Task<AnimalModel> AddAnimal(AddAnimalModel model);

    //Task UpdateAnimal(int id, UpdateAnimalModel model);
    //Task DeleteAnimal(int animalId);

}

