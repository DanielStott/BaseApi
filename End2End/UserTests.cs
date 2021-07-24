namespace End2End
{
    using System.Threading.Tasks;
    using BaseApi.Controllers.Users;
    using Domain.Users.Handlers;
    using NUnit.Framework;

    public class UserTests
    {
        [Test]
        public async Task successfully_create_user()
        {
            var command =
                new CreateUser.Command("test", "test@test.com", "password", "test", "test");
            var user = await TestApi.Post<CreateUser.Command, UserViewModel>(nameof(UsersController.Create), command);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(command.Email, user.Email);
                Assert.IsNotNull(user.Id);
            });
        }
    }
}