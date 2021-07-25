namespace Api.Controllers.Users
{
    using AutoMapper;
    using Domain.Users.Models;

    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}