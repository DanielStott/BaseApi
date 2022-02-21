using System;
using Domain.Shared.Interfaces;

namespace Domain.Shared.Exceptions;

public class AlreadyExistsExceptions : Exception, ICustomException
{
    public string Title { get; }
    public int StatusCode { get; }
    public string Details { get; }

    public AlreadyExistsExceptions(string typeName)
    {
        Title = $"{typeName} Already Exists.";
        StatusCode = 400;
        Details = $"{typeName} already exists, please try again with a different {typeName}";
    }
}