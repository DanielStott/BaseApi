﻿using System.Net;
using System.Threading.Tasks;
using Api.Controllers.Users;
using Domain.Users.Handlers;
using NUnit.Framework;

namespace Test.E2E.Users;

[TestFixture]
public class UserTests
{
    [Test]
    public async Task successfully_create_user()
    {
        var command = new CreateUser.Command
        {
            Username = "test",
            Email = "test@test.com",
            Password = "password",
            FirstName = "test",
            LastName = "test",
        };

        var (user, httpResponseCode) = await TestApi.Post<CreateUser.Command, UserViewModel>(nameof(UsersController.Create), command);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(httpResponseCode, HttpStatusCode.OK);
            Assert.AreEqual(command.Email, user.Email);
            Assert.IsNotNull(user.Id);
        });
    }

    [Test]
    public async Task successfully_get_user()
    {
        var (user, httpResponseCode) = await TestApi.Get<UserViewModel>(nameof(UsersController.Get), "userId", Data.Users.Morty.Id);

        Assert.Multiple(() =>
        {
            Assert.AreEqual(HttpStatusCode.OK, httpResponseCode);
            Assert.AreEqual( Data.Users.Morty.Email, user.Email);
            Assert.IsNotNull(user.Id);
        });
    }
}