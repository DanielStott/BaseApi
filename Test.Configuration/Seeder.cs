using Data;
using Storage.Users;

namespace Test.Configuration;

public class Seeder
{
    private readonly UserContext _userContext;

    public Seeder(UserContext userContext) => _userContext = userContext;

    public async Task Seed()
    {
        await _userContext.BuildTable();
        await _userContext.AddRange(TestSuites.GetAll(x => x.Users));
    }
}