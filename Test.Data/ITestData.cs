using System.Collections.Generic;

namespace Test.Data;

public interface ITestData<T> where T : class
{
    IEnumerable<T> All { get; set; }
}