namespace FindPets.API.Controllers.Requests;

using AutoMapper;
using FindPets.API.Controllers.Models;
using FindPets.API.Controllers.Requests.Models;
using FindPets.Services.Requests;
using Microsoft.AspNetCore.Mvc;

[Route("api/v{version:apiVersion}/requests")]
[ApiController]
[ApiVersion("1.0")]
public class RequestsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<RequestsController> logger;
    private readonly IRequestService requestService;
    private readonly IWebHostEnvironment webHostEnvironment;


    public RequestsController(IMapper mapper, ILogger<RequestsController> logger, IRequestService requestService, IWebHostEnvironment webHostEnvironment)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.requestService = requestService;
        this.webHostEnvironment = webHostEnvironment;
    }


    /// <summary>
    /// Add request
    /// </summary>
    /// <param name="request"></param>
    /// <response code="200">AddRequestRequest</response>
    [HttpPost("")]
    public async Task<RequestResponse> AddAnimal([FromBody] AddRequestRequest request)
    {
        var model = mapper.Map<AddRequestModel>(request);
        var requestS = await requestService.AddRequest(model);
        var response = mapper.Map<RequestResponse>(requestS);

        return response;
    }

    /// <summary>
    /// Get requests
    /// </summary>
    /// <param name="offset">Offset to the first element</param>
    /// <param name="limit">Count elements on the page</param>
    /// <response code="200">List of requests for lost animals</response>
    [ProducesResponseType(typeof(IEnumerable<RequestResponse>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<RequestResponse>> GetRequests([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var requests = await requestService.GetRequests(offset, limit);
        var response = mapper.Map<IEnumerable<RequestResponse>>(requests);

        return response;
    }




}
