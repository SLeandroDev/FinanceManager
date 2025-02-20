using FinanceManager.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
public static class ExceptionHandler
{
    public static int GetStatusCodeForException(Exception ex)
    {
        return ex switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            BusinessException => StatusCodes.Status422UnprocessableEntity,
            NotFoundException => StatusCodes.Status404NotFound,
            ConflictException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
