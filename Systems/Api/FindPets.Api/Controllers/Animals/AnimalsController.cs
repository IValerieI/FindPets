namespace FindPets.API.Controllers.Animals;

using AutoMapper;
using FindPets.API.Controllers.Animals.Models;
using FindPets.API.Controllers.Models;
//using FindPets.Common.Security;
using FindPets.Services.Animals;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/v{version:apiVersion}/animals")]
[ApiController]
[ApiVersion("1.0")]
public class AnimalsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<AnimalsController> logger;
    private readonly IAnimalService animalService;

    public AnimalsController(IMapper mapper, ILogger<AnimalsController> logger, IAnimalService animalService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.animalService = animalService;
    }


    /// <summary>
    /// Get animals
    /// </summary>
    /// <param name="offset">Offset to the first element</param>
    /// <param name="limit">Count elements on the page</param>
    /// <response code="200">List of AnimalResponses</response>
    [ProducesResponseType(typeof(IEnumerable<AnimalResponse>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<AnimalResponse>> GetAnimals([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var animals = await animalService.GetAnimals(offset, limit);
        var response = mapper.Map<IEnumerable<AnimalResponse>>(animals);

        return response;
    }


    /// <summary>
    /// Get animal by Id
    /// </summary>
    /// <response code="200">AnimalResponse</response>
    [ProducesResponseType(typeof(AnimalResponse), 200)]
    [HttpGet("{id}")]
    public async Task<AnimalResponse> GetAnimalById([FromRoute] int id)
    {
        var animal = await animalService.GetAnimal(id);
        var response = mapper.Map<AnimalResponse>(animal);

        return response;
    }

    /// <summary>
    /// Add animal
    /// </summary>
    /// <response code="200">AddAnimalRequest</response>
    [HttpPost("")]
    public async Task<AnimalResponse> AddAnimal([FromBody] AddAnimalRequest request)
    {
        var model = mapper.Map<AddAnimalModel>(request);
        var animal = await animalService.AddAnimal(model);
        var response = mapper.Map<AnimalResponse>(animal);

        return response;
    }


    /// <summary>
    /// Update animal by Id
    /// </summary>
    /// <response code="200">UpdateAnimalRequest</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnimal([FromRoute] int id, [FromBody] UpdateAnimalRequest request)
    {
        var model = mapper.Map<UpdateAnimalModel>(request);
        await animalService.UpdateAnimal(id, model);

        return Ok();
    }


    /// <summary>
    /// Delete animal by Id
    /// </summary>
    /// <response code="200">Deletes an animal by Id</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal([FromRoute] int id)
    {
        await animalService.DeleteAnimal(id);

        return Ok();
    }


}