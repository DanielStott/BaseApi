namespace Data
{
    using System.Collections.Generic;
    using Domain.Users.Models;

    public class Users : ITestData<User>
    {
        public IEnumerable<User> All { get; set; } = new List<User> { Rick, Morty };
        public static User Rick { get; }
        public static User Morty { get; }

        static Users()
        {
           Rick = User.Create("Rick", "Rick@test.com", "Password", "Rick", "Sanchez");
           Morty = User.Create("Morty", "Morty@test.com", "Password", "Morty", "Sanchez");
        }
    }
}