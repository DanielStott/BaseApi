namespace Domain.Shared.Exceptions
{
    using System;
    using Domain.Shared.Interfaces;

    public class AlreadyExistsExceptions : Exception, ICustomException
    {
        public string Title { get; set; }
        public int StatusCode { get; set; }
        public string Details { get; set; }
    }
}