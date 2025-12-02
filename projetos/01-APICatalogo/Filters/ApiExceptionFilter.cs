using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace _01_APICatalogo.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public readonly ILogger<ApiExceptionFilter> _logger;


    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "============ Ocorreu uma exceção não tratada: Status Code 500 ============");

        context.Result = new ObjectResult("Ocorreu um problema ao tratar a sua solicitação: Status Code 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
    }
}
