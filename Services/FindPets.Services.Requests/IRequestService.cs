namespace FindPets.Services.Requests;

public interface IRequestService
{
    Task<IEnumerable<RequestModel>> GetRequests(int offset = 0, int limit = 10);
    Task<RequestModel> AddRequest(AddRequestModel model);

}
