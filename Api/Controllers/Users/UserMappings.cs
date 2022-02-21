using AutoMapper;
using Domain.Users.Models;

namespace Api.Controllers.Users;

public class UserMappings : Profile
{
    public UserMappings()
    {
        CreateMap<User, UserViewModel>();
    }
}