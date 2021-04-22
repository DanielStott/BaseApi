using Domain.Shared;
using Domain.Shared.Interfaces;
using Domain.Users.Models;

namespace Domain.Users.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
    }
}