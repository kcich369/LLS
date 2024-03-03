namespace LLS.Identity.Domain.Results;

public static class ResultExtensions
{
    public static async Task<IResult<T>> TryCatch<T>(this Task<IResult<T>> task, string errorMsg)
    {
        try
        {
            return await task;
        }
        catch
        {
            return Result<T>.Error(errorMsg);
        }
    }
}