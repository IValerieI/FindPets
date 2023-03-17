using Microsoft.Extensions.DependencyInjection;

namespace FindPets.Services.EmailSender;

public static class Bootstrapper
{
    public static IServiceCollection AddEmailSenderService(this IServiceCollection services)
    {
        services.AddSingleton<IEmailSenderService, EmailSenderService>();

        return services;
    }
}
