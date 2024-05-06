namespace Cloupard.Domain.Results;

public sealed record Result(bool IsSuccess, int StatusCode, string Message = null, IEnumerable<string> Errors = null)
    : BaseResult(IsSuccess, StatusCode, Message, Errors)
{
    public static Result Success(int statusCode = 200, string message = "Operation completed successfully") => new (true, statusCode, message);
    public static Result Error(int statusCode, string message, IEnumerable<string> errors = null) => new (false, statusCode, Message: message, Errors: errors);
}

public sealed record Result<TData>(bool IsSuccess, int StatusCode, TData Data = default, string Message = null, IEnumerable<string> Errors = null)
    : BaseResult(IsSuccess, StatusCode, Message, Errors) where TData : class
{
    public static Result<TData> Success(TData data, int statusCode = 200, string message = "Operation completed successfully") => new(true, statusCode, data, message);
    public static Result<TData> Error(int statusCode, string message, IEnumerable<string> errors = null) => new (false, statusCode, Message: message, Errors: errors);
}