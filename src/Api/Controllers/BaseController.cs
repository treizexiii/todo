using Api.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Tools.TransactionManager;

namespace Api.Controllers;

public abstract class BaseController(ILogger<BaseController> logger, ITransactionManager transaction)
    : ControllerBase
{
    protected readonly ILogger<BaseController> Logger = logger;
    protected readonly ITransactionManager Transaction = transaction;


    protected IActionResult Ok<T>(T data)
    {
        var response = new ApiResponse<T>()
        {
            Version = "1.0",
            Code = 200,
            Success = true,
            Message = "OK",
            Data = data
        };

        return base.Ok(response);
    }

    protected IActionResult Ok(string message)
    {
        var response = new ApiResponse()
        {
            Version = "1.0",
            Code = 200,
            Success = true,
            Message = "OK"
        };

        return base.Ok(response);
    }

    protected IActionResult Error(Exception e)
    {
        var exceptionType = e.GetType().Name;
        int code;
        switch (exceptionType)
        {
            case "DataException":
            case "ArgumentException":
                code = 400;
                break;
            case "KeyNotFoundException":
            case "FileNotFoundException":
                code = 404;
                break;
            default:
                code = 500;
                break;
        }

        var response = new ApiResponse
        {
            Version = "1.0",
            Code = code,
            Success = false,
            Message = e.Message
        };

        return base.StatusCode(response.Code, response);
    }
}