using System;
using System.Collections.Generic;
using System.Linq;

namespace Data;

public static class TestSuites
{
    public static readonly RickAndMorty RickAndMorty = new ();
    public static readonly RickAndMorty SouthPark = new ();
    public static readonly IEnumerable<ITestSuite> All = new List<ITestSuite> { RickAndMorty };

    public static IEnumerable<T> GetAll<T>(Func<ITestSuite, ITestData<T>> predicate) where T : class
        => All.SelectMany(testSuite => predicate(testSuite).All);
}