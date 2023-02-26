namespace FindPets.API.Controllers.Animals;

using AutoMapper;
using FindPets.API.Controllers.Animals.Models;
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

    //[HttpGet("")]
    ////[Authorize(AppScopes.BooksRead)]
    //public async Task<IEnumerable<AnimalResponse>> GetAnimals()
    //{
    //    var animals = await animalService.GetAnimals();
    //    var response = mapper.Map<IEnumerable<AnimalResponse>>(animals);

    //    return response;
    //}




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



}