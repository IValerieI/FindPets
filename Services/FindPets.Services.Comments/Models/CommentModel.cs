namespace FindPets.Services.Comments;

using AutoMapper;
using FindPets.Context.Entities;

public class CommentModel
{
    public int Id { get; set; }
    public int AnimalId { get; set; }

    public string Animal { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}

public class CommentModelProfile : Profile
{
    public CommentModelProfile()
    {
        CreateMap<Comment, CommentModel>();
        //.ForMember(dest => dest.Animal, opt => opt.MapFrom(src => src.Animal.Kind));
    }
}