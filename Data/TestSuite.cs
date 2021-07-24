using System;
using Domain.Users.Models;

namespace Data
{
    public class TestSuite : ITestSuite
    {
        public ITestData<User> Users { get; init; }
    }
}