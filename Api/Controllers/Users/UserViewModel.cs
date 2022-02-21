using System;

namespace Api.Controllers.Users;

public class UserViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; }
}