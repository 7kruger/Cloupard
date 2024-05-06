namespace Cloupard.Domain.Results;

public record BaseResult(bool IsSuccess, int StatusCode, string Message = null, IEnumerable<string> Errors = null);

