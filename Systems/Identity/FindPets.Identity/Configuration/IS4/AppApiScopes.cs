namespace FindPets.Identity.Configuration;

using Duende.IdentityServer.Models;
using FindPets.Common.Security;

public static class AppApiScopes
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope(AppScopes.AnimalsRead, "Access to animals API - Read data"),
            new ApiScope(AppScopes.AnimalsWrite, "Access to animals API - Write data")
        };
}