namespace End2End.Users
{
    using System.Threading.Tasks;
    using Api.Controllers.Users;
    using Domain.Users.Handlers;
    using NUnit.Framework;

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

            var user = await TestApi.Post<CreateUser.Command, UserViewModel>(nameof(UsersController.Create), command);

            Assert.Multiple(() =>
            {
                Assert.AreEqual(command.Email, user.Email);
                Assert.IsNotNull(user.Id);
            });
        }
    }
}