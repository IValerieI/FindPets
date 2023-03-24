namespace FindPets.API.Controllers.Animals;

using AutoMapper;
using FindPets.API.Controllers.Animals.Models;
using FindPets.API.Controllers.Models;
using FindPets.Services.Animals;
using Microsoft.AspNetCore.Mvc;

[Route("api/v{version:apiVersion}/animals")]
[ApiController]
[ApiVersion("1.0")]
public class AnimalsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<AnimalsController> logger;
    private readonly IAnimalService animalService;
    private readonly IWebHostEnvironment webHostEnvironment;

    public AnimalsController(IMapper mapper, ILogger<AnimalsController> logger, IAnimalService animalService, IWebHostEnvironment webHostEnvironment)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.animalService = animalService;
        this.webHostEnvironment = webHostEnvironment;
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
    /// Get photo of animal by Id
    /// </summary>
    /// <response code="200">Photo of animal</response>
    [Produces("image/png", "application/json")]
    [HttpGet("photo/{id}")]
    public async Task<IActionResult> GetPhoto([FromRoute] int id)
    {
        var animal = await animalService.GetAnimal(id);
        string path = webHostEnvironment.WebRootPath + animal.Image;
        if (System.IO.File.Exists(path))
        {
            byte[] b = System.IO.File.ReadAllBytes(path);
            return File(b, "image/png");
        }
        return null;


    }

    /// <summary>
    /// Add animal
    /// </summary>
    /// <param name="request"></param>
    /// <response code="200">AddAnimalRequest</response>
    [HttpPost("")]
    public async Task<AnimalResponse> AddAnimal([FromForm] AddAnimalRequest request)
    {

        if (request.File != null)
        {
            string path = "/photos/" + Path.GetRandomFileName();
            using (var fileStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create))
            {
                await request.File.CopyToAsync(fileStream);
            }
            request.Image = path;

            var model = mapper.Map<AddAnimalModel>(request);
            var animal = await animalService.AddAnimal(model);
            var response = mapper.Map<AnimalResponse>(animal);

            return response;
        }


        return null;
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




