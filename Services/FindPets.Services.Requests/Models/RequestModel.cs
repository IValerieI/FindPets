using AutoMapper;
using FindPets.Context.Entities;

namespace FindPets.Services.Requests;

public class RequestModel
{
    public int AnimalId { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

}


public class RequestModelProfile : Profile
{
    public RequestModelProfile()
    {
        CreateMap<Request, RequestModel>();
    }
}


