using System.Collections.Generic;
using Domain.Users.Models;

namespace Test.Data;

public class Users : ITestData<User>
{
    public IEnumerable<User> All { get; set; } = new List<User> { Rick, Morty };
    public static User Rick { get; }
    public static User Morty { get; }

    static Users()
    {
        Rick = new User("Rick", "Rick@test.com", "Password", "Rick", "Sanchez");
        Morty = new User("Morty", "Morty@test.com", "Password", "Morty", "Smith");
    }
}