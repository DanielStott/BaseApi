using System.Threading.Tasks;
using Domain.Shared.Interfaces;
using Domain.Users.Models;

namespace Domain.Users.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByEmailOrUsername(string email, string username);
}