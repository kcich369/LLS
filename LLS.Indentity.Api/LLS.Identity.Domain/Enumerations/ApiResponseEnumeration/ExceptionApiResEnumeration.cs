using LLS.Identity.Domain.Enumerations.Base;

namespace LLS.Identity.Domain.Enumerations.ApiResponseEnumeration;

public class ExceptionApiResEnumeration : ApiResponseTypesEnumerations
{
    public ExceptionApiResEnumeration Exception = new(0, nameof(Exception), StatusCodeEnumerations.InternalServerError);

    protected ExceptionApiResEnumeration(int errorCode, string errorMessageTranslation,
        StatusCodeEnumerations statusCode) : base(500, "Internal server error", errorCode, errorMessageTranslation,
        statusCode)
    {
    }
}