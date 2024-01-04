namespace LLS.Identity.Domain.Results;

public interface IResult
{
    public bool IsError { get; }
    public string ErrorMessage { get; }
}

public interface IResult<out T> : IResult
{
    public T Data { get; }
}