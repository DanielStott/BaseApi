namespace Data
{
    using System.Collections.Generic;

    public interface ITestData<T> where T : class
    {
        IEnumerable<T> All { get; set; }
    }
}