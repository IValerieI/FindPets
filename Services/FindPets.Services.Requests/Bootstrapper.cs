using Microsoft.Extensions.DependencyInjection;

namespace FindPets.Services.Requests;

public static class Bootstrapper
{
    public static IServiceCollection AddRequestService(this IServiceCollection services)
    {
        services.AddSingleton<IRequestService, RequestService>();

        return services;
    }

}


