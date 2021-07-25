namespace Domain.Shared.Interfaces
{
    public interface ICustomException
    {
        string Title { get; set; }
        int StatusCode { get; set; }
        string Details { get; set; }
    }
}