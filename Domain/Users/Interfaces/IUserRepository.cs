namespace Domain.Users.Interfaces
{
    using System.Threading.Tasks;
    using Domain.Shared.Interfaces;
    using Domain.Users.Models;

    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailOrUsername(string email, string username);
    }
}