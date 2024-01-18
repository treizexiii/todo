namespace Authentication.Domain.Exceptions;

public enum ResultCode
{
    Success = 200,
    NotFound = 404,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    AlreadyExists = 409,
    InternalServerError = 500,
    NotImplemented = 501,
    ServiceUnavailable = 503,
}