using AutoMapper;
using Domain.Users.Models;

namespace BaseApi.Controllers.Users
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}