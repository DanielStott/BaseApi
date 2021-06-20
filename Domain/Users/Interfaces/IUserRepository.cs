namespace Domain.Users.Interfaces
{
    using Domain.Shared.Interfaces;
    using Domain.Users.Models;

    public interface IUserRepository : IRepository<User>
    {
    }
}