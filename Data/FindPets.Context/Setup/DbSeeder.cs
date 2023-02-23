namespace FindPets.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider) => serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    private static MainDbContext DbContext(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();

    //private static readonly string masterUserName = "Admin";
    //private static readonly string masterUserEmail = "admin@dsrnetscool.com";
    //private static readonly string masterUserPassword = "Pass123#";

    //private static void ConfigureAdministrator(IServiceScope scope)
    //{
    //    //    var defaults = scope.ServiceProvider.GetService<IDefaultsSettings>();

    //    //    if (defaults != null)
    //    //    {
    //    //        var userService = scope.ServiceProvider.GetService<IUserService>();
    //    //        if (addAdmin && (!userService?.Any() ?? false))
    //    //        {
    //    //            var user = userService.Create(new CreateUserModel
    //    //            {
    //    //                Type = UserType.ForPortal,
    //    //                Status = UserStatus.Active,
    //    //                Name = defaults.AdministratorName,
    //    //                Password = defaults.AdministratorPassword,
    //    //                Email = defaults.AdministratorEmail,
    //    //                IsEmailConfirmed = !defaults.AdministratorEmail.IsNullOrEmpty(),
    //    //                Phone = null,
    //    //                IsPhoneConfirmed = false,
    //    //                IsChangePasswordNeeded = true
    //    //            })
    //    //                .GetAwaiter()
    //    //                .GetResult();

    //    //            userService.SetUserRoles(user.Id, Infrastructure.User.UserRole.Administrator).GetAwaiter().GetResult();
    //    //        }
    //    //    }
    //}

    public static void Execute(IServiceProvider serviceProvider, bool addDemoData, bool addAdmin = true)
    {
        using var scope = ServiceScope(serviceProvider);
        ArgumentNullException.ThrowIfNull(scope);

        //if (addAdmin)
        //{
        //    ConfigureAdministrator(scope);
        //}

        if (addDemoData)
        {
            Task.Run(async () =>
            {
                await ConfigureDemoData(serviceProvider);
            });
        }
    }

    private static async Task ConfigureDemoData(IServiceProvider serviceProvider)
    {
        await AddAnimals(serviceProvider);
    }

    private static async Task AddAnimals(IServiceProvider serviceProvider)
    {
        await using var context = DbContext(serviceProvider);

        if (context.Animals.Any() || context.Comments.Any() || context.Requests.Any())
            return;

        var c1 = new Entities.Comment()
        {
            Name = "Lisa",
            Text = "I think I saw Ginger today. He was playing with another cats in the woods.",
        };
        context.Comments.Add(c1);

        var a1 = new Entities.Animal()
        {
            Kind = "Cat",
            Breed = "Maine coon",
            Description = "Ginger got lost somewhere near the local book shop",
            Image = ".../imgs/Ginger.png",
            LostSince = DateTime.Now,
            Comments = new List<Entities.Comment>() { c1 },

        };
        context.Animals.Add(a1);

        c1.Animal = a1;

        var c2 = new Entities.Comment()
        {
            Name = "Mike",
            Text = "Today Fluffy stole a piece of meat from a butcher shop!",
        };
        context.Comments.Add(c2);

        var a2 = new Entities.Animal()
        {
            Kind = "Dog",
            Breed = "Poodle",
            Description = "Fluffy got lost somewhere near the local butcher shop",
            Image = ".../imgs/Fluffy.png",
            LostSince = DateTime.Now
        };
        context.Animals.Add(a2);

        c2.Animal = a2;

        var r1 = new Entities.Request()
        {
            Name = "Sophie",
            Phone = "+7-950-987-42-58",
            Animal = a1,

        };
        context.Requests.Add(r1);

        context.SaveChanges();
    }
}
