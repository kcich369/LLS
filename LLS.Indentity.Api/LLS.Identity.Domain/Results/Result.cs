namespace LLS.Identity.Domain.Results;

public class Result : IResult
{
    public bool IsError { get; private set; }
    public string ErrorMessage { get; private set; }

    protected Result()
    {
    }

    protected Result( string errorMessage)
    {
        ErrorMessage = errorMessage;
        IsError = true;
    }
}

public class Result<T> : Result, IResult<T>
{
    public T Data { get; private set; }

    private Result(T data)
    {
        Data = data;
    }

    private Result( string errorMessage) : base( errorMessage)
    {
    }

    public static Result<T> Success(T data) => new Result<T>(data);
    
    public static Result<T> Error(IResult errorResult)
    {
        if (!errorResult.IsError)
            throw new ArgumentException("Data is not error");
        return new Result<T>(errorResult.ErrorMessage);
    }

    public static Result<T> Error(string errorMessage = null) =>
        new Result<T>( errorMessage);
}