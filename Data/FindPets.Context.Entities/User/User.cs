namespace FindPets.Context.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public UserStatus Status { get; set; }
}