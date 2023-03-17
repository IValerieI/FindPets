namespace FindPets.Services.Comments;

using AutoMapper;
using FindPets.Common.Validator;
using FindPets.Context;
using FindPets.Context.Entities;
using Microsoft.EntityFrameworkCore;

public class CommentService : ICommentService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddCommentModel> addCommentModelValidator;

    public CommentService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddCommentModel> addCommentModelValidator
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.addCommentModelValidator = addCommentModelValidator;
    }


    public async Task<IEnumerable<CommentModel>> GetComments(int offset = 0, int limit = 10)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var comments = context
            .Comments
            .AsQueryable();

        comments = comments
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var data = (await comments.ToListAsync()).Select(comment => mapper.Map<CommentModel>(comment));

        return data;
    }



    public async Task<CommentModel> AddComment(AddCommentModel model)
    {
        addCommentModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var comment = mapper.Map<Comment>(model);
        await context.Comments.AddAsync(comment);
        context.SaveChanges();

        return mapper.Map<CommentModel>(comment);

    }


}