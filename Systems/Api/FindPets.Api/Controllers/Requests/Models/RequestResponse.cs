namespace FindPets.API.Controllers.Requests.Models;

using AutoMapper;
using FindPets.Services.Requests;
using System.ComponentModel.DataAnnotations;

public class RequestResponse
{
    public int AnimalId { get; set; }

    public string Name { get; set; } = string.Empty;
    [Phone]
    public string Phone { get; set; } = string.Empty;
}


public class RequestResponseProfile : Profile
{
    public RequestResponseProfile()
    {
        CreateMap<RequestModel, RequestResponse>();
    }
}

