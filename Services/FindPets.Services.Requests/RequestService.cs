using AutoMapper;
using FindPets.Common.Validator;
using FindPets.Context;
using FindPets.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace FindPets.Services.Requests;


public class RequestService : IRequestService
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;
    private readonly IMapper mapper;
    private readonly IModelValidator<AddRequestModel> addRequestModelValidator;

    public RequestService(
        IDbContextFactory<MainDbContext> contextFactory,
        IMapper mapper,
        IModelValidator<AddRequestModel> addRequestModelValidator
        )
    {
        this.contextFactory = contextFactory;
        this.mapper = mapper;
        this.addRequestModelValidator = addRequestModelValidator;
    }

    public async Task<RequestModel> AddRequest(AddRequestModel model)
    {
        addRequestModelValidator.Check(model);

        using var context = await contextFactory.CreateDbContextAsync();

        var request = mapper.Map<Request>(model);
        await context.Requests.AddAsync(request);
        context.SaveChanges();

        return mapper.Map<RequestModel>(request);
    }

    public async Task<IEnumerable<RequestModel>> GetRequests(int offset = 0, int limit = 10)
    {
        using var context = await contextFactory.CreateDbContextAsync();

        var requests = context
            .Requests
            .AsQueryable();

        requests = requests
            .Skip(Math.Max(offset, 0))
            .Take(Math.Max(0, Math.Min(limit, 1000)));

        var data = (await requests.ToListAsync()).Select(request => mapper.Map<RequestModel>(request));

        return data;

    }
}
