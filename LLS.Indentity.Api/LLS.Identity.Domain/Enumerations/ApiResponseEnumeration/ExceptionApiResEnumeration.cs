using LLS.Identity.Domain.Enumerations.Base;

namespace LLS.Identity.Domain.Enumerations.ApiResponseEnumeration;

public class ExceptionApiResEnumeration : ApiResponseTypesEnumerations
{
    public ExceptionApiResEnumeration Exception = new(0, nameof(Exception));

    protected ExceptionApiResEnumeration(int errorCode, string errorMessage)
        : base(500, "Internal server error", errorCode, errorMessage, StatusCodeEnumerations.InternalServerError)
    {
    }
}