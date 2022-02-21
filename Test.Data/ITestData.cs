using System.Collections.Generic;

namespace Data;

public interface ITestData<T> where T : class
{
    IEnumerable<T> All { get; set; }
}