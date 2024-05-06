using System.Net;
using Cloupard.Domain.Results;

namespace Cloupard.Application.Validations;

public abstract class BaseValidator<T> where T : class
{
    public Result ValidateOnNull(T entity)
    {
        if (entity == null)
        {
            return Result.Error((int)HttpStatusCode.NotFound, "Not found");
        }

        return Result.Success();
    }
}