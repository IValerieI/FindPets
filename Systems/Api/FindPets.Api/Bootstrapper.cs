namespace FindPets.Api;


using FindPets.Api.Settings;
using FindPets.Services.Animals;
using FindPets.Services.Comments;
using FindPets.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddApiSpecialSettings()
            .AddAnimalService()
            .AddCommentService()
            ;

        return services;
    }
}
