namespace FindPets.API.Controllers.Comments;

using AutoMapper;
using FindPets.API.Controllers.Models;
using FindPets.Common.Responses;
using FindPets.Services.Comments;
using Microsoft.AspNetCore.Mvc;


/// <summary>
/// Comments controller
/// </summary>
/// <response code="400">Bad Request</response>
/// <response code="401">Unauthorized</response>
/// <response code="403">Forbidden</response>
/// <response code="404">Not Found</response>
[ProducesResponseType(typeof(ErrorResponse), 400)]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/comments")]
[ApiController]
[ApiVersion("1.0")]
public class CommentsController : ControllerBase
{
    private readonly IMapper mapper;
    private readonly ILogger<CommentsController> logger;
    private readonly ICommentService commentService;

    public CommentsController(IMapper mapper, ILogger<CommentsController> logger, ICommentService commentService)
    {
        this.mapper = mapper;
        this.logger = logger;
        this.commentService = commentService;
    }


    /// <summary>
    /// Get comments
    /// </summary>
    /// <param name="animalId">Animal Id</param>
    /// <param name="offset">Offset to the first element</param>
    /// <param name="limit">Count elements on the page</param>
    /// <response code="200">List of CommentResponses</response>
    [ProducesResponseType(typeof(IEnumerable<CommentResponse>), 200)]
    [HttpGet("")]
    public async Task<IEnumerable<CommentResponse>> GetComments([FromQuery] int offset = 0, [FromQuery] int limit = 10)
    {
        var comments = await commentService.GetComments(offset, limit);
        var response = mapper.Map<IEnumerable<CommentResponse>>(comments);

        return response;
    }



    /// <summary>
    /// Add comment
    /// </summary>
    /// <param name="request"></param>
    /// <response code="200">AddCommentRequest</response>
    //[Produces("multipart/form-data")]
    [HttpPost("")]
    //[Route("testAnimal/")]
    public async Task<CommentResponse> AddComment([FromForm] AddCommentRequest request)
    {
        //request.Created = DateTime.UtcNow;
        var model = mapper.Map<AddCommentModel>(request);
        var comment = await commentService.AddComment(model);
        var response = mapper.Map<CommentResponse>(comment);

        return response;
    }


}

