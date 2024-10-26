namespace Core.Utils;

public class Result<T>
{
    public T? Value { get; init; }
    public string? ErrorMessage { get; init; }
    public bool IsSuccess => StatusCode >= 200 && StatusCode < 400;
    public int StatusCode { get; init; }

    public Result(T? value, int statusCode, string? error = null)
    {
        Value = value;
        ErrorMessage = error;
        StatusCode = statusCode;
    }

    public static Result<T> Success(T value, int statusCode = 200) => new Result<T>(value, statusCode); 

    public static Result<T> Failure(string errorMessage, int statusCode) =>  new Result<T> (default, statusCode, errorMessage);
}