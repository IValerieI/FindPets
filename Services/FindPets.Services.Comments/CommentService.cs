namespace FindPets.Services.Comments;

using AutoMapper;
using FindPets.Context;
using Microsoft.EntityFrameworkCore;

public class CommentService : ICommentService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    //private readonly IModelValidator<AddCommentModel> addCommentModelValidator;

    public CommentService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper
        //IModelValidator<AddCommentModel> addCommentModelValidator
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        //this.addCommentModelValidator = addCommentModelValidator;
    }


    public async Task<IEnumerable<CommentModel>> GetComments(int offset = 0, int limit = 10)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var comments = context
            .Comments
            //.Include(x => x.Animal)
            .AsQueryable();

        comments = comments
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var data = (await comments.ToListAsync()).Select(comment => mapper.Map<CommentModel>(comment));

        return data;
    }



    //public async Task<CommentModel> AddComment(int animalId, AddCommentModel model)
    //{
    //    throw new NotImplementedException();
    //}


}