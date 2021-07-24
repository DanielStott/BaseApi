namespace End2End
{
    using System.Threading.Tasks;
    using BaseApi.Controllers.Users;
    using NUnit.Framework;

    public class UserTests
    {
        [Test]
        public async Task successfully_create_user()
        {
            await TestApi.Get<UserViewModel>("");
        }
    }
}