using System;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Test.Integration.Users;

[TestFixture]
public class UserTests : BaseTest
{
    [Test]
    public async Task successfully_create_user()
    {
        var command = new
        {
            Username = "Test",
            Email = "test@test.com",
            Password = "password",
            FirstName = "Daniel",
            LastName = "Test",
        };

        var (response, user) = await Api.Post("/api/users", command);

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That((string)user.email, Is.EqualTo(command.Email));
            Assert.That(user.id, Is.Not.Null);
        });
    }

    [Test]
    public async Task successfully_get_user()
    {
        var (response, user) = await Api.Get($"/api/users/{Data.Users.Rick.Id}");

        Assert.Multiple(() =>
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That((Guid)user.id, Is.EqualTo(Data.Users.Rick.Id));
            Assert.That((string)user.email, Is.EqualTo(Data.Users.Rick.Email));
        });
    }
}