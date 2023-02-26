namespace FindPets.Services.Animals;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddAnimalService(this IServiceCollection services)
    {
        services.AddSingleton<IAnimalService, AnimalService>();

        return services;
    }
}
