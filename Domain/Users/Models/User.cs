using System;

namespace Domain.Users.Models
{
    public class User
    {
        public Guid Id { get; private init; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public static User Create(string email, string password, string firstName, string lastName) 
            => new()
            {
                Id = Guid.NewGuid(),
                Email = email,
                Password = password,
                FirstName = firstName,
                LastName = lastName
            };
    }
}