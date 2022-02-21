namespace Domain.Shared.Interfaces;

public interface ICustomException
{
    string Title { get; }
    int StatusCode { get; }
    string Details { get; }
}