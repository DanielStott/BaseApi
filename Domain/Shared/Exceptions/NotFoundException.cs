using System;
using System.Net;
using Domain.Shared.Interfaces;

namespace Domain.Shared.Exceptions;

public class NotFoundException : Exception, ICustomException
{
    public string Title { get; }
    public int StatusCode { get; }
    public string Details { get; }

    public NotFoundException(string typeName)
    {
        Title = $"{typeName} not found.";
        StatusCode = (int)HttpStatusCode.NotFound;
        Details = $"{typeName} could not be found.";
    }
}