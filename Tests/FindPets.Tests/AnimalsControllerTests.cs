using AutoMapper;
using FakeItEasy;
using FindPets.API.Controllers.Animals;
using FindPets.API.Controllers.Animals.Models;
using FindPets.Services.Animals;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Xunit;


namespace FindPets.Tests;

public class AnimalsControllerTests
{
    [Fact]
    public async Task GetAnimals_Returns_The_Correct_Number_Of_Animals()
    {
        // Arrange

        int count = 10;
        var animalModels = A.CollectionOfDummy<AnimalModel>(count).AsEnumerable();
        var animalResponses = A.CollectionOfDummy<AnimalResponse>(count).AsEnumerable();

        var animalService = A.Fake<IAnimalService>();
        A.CallTo(() => animalService.GetAnimals(0, count)).Returns(Task.FromResult(animalModels));

        var mapper = A.Fake<IMapper>();
        A.CallTo(() => mapper.Map<IEnumerable<AnimalResponse>>(animalModels)).Returns(animalResponses);

        var logger = A.Fake<ILogger<AnimalsController>>();
        var webHostEnv = A.Fake<IWebHostEnvironment>();
        var controller = new AnimalsController(
            mapper,
            logger,
            animalService,
            webHostEnv);

        // Act

        var result = await controller.GetAnimals(0, count);
        Task taskResult = controller.GetAnimals(0, count);

        // Assert

        Assert.Equal(count, result.Count());
        Assert.True(taskResult.IsCompletedSuccessfully);

    }

    [Fact]
    public async Task GetAnimalById_Returns_Animal_With_The_Same_Id()
    {
        // Arrange

        int id = 10;

        var animalModel = A.Fake<AnimalModel>();
        animalModel.Id = id;

        var animalResponse = A.Fake<AnimalResponse>();
        animalResponse.Id = id;

        var animalService = A.Fake<IAnimalService>();
        A.CallTo(() => animalService.GetAnimal(id)).Returns(Task.FromResult(animalModel));

        var mapper = A.Fake<IMapper>();
        A.CallTo(() => mapper.Map<AnimalResponse>(animalModel)).Returns(animalResponse);

        var logger = A.Fake<ILogger<AnimalsController>>();
        var webHostEnv = A.Fake<IWebHostEnvironment>();
        var controller = new AnimalsController(
            mapper,
            logger,
            animalService,
            webHostEnv);


        // Act

        var result = await controller.GetAnimalById(id);
        Task taskResult = controller.GetAnimalById(id);

        // Assert

        Assert.Equal(id, result.Id);
        Assert.True(taskResult.IsCompletedSuccessfully);

    }


















}
