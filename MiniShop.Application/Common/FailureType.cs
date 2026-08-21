namespace MiniShop.Application.Common;

public enum FailureType
{
    ValidationError,
    NotFound,
    Conflict,
    Unauthorized,
    Forbidden,
    BadRequest,
    InternalServerError
}