namespace FindPets.API.Controllers.Models;

using AutoMapper;
using FindPets.Services.Comments;

public class CommentResponse
{
    /// <summary>
    /// Comment Id
    /// </summary>
    public int Id { get; set; }
    public int AnimalId { get; set; }

    public string Animal { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;

}

public class CommentResponseProfile : Profile
{
    public CommentResponseProfile()
    {
        CreateMap<CommentModel, CommentResponse>();
    }
}