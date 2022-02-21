using Domain.Shared.Models;

namespace Domain.Users.Models;

public class User : Entity
{
    public string Username { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public static User Create(string username, string email, string password, string firstName, string lastName)
        => new ()
        {
            Username = username,
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
        };
}