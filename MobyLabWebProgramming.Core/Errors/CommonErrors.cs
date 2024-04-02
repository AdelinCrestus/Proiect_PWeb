using System.Net;

namespace MobyLabWebProgramming.Core.Errors;

/// <summary>
/// Common error messages that may be reused in various places in the code.
/// </summary>
public static class CommonErrors
{
    public static ErrorMessage UserNotFound => new(HttpStatusCode.NotFound, "User doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TableNotFound => new(HttpStatusCode.NotFound, "Table doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage TableNotAvailable => new(HttpStatusCode.Forbidden, "All tables of this type are taken!", ErrorCodes.CannotAdd);
    public static ErrorMessage LocationNotFound => new(HttpStatusCode.NotFound, "Location doesn't exist!", ErrorCodes.EntityNotFound);
    public static ErrorMessage LocationClosed => new(HttpStatusCode.Forbidden, "Location is closed!", ErrorCodes.CannotAdd);

    public static ErrorMessage FileNotFound => new(HttpStatusCode.NotFound, "File not found on disk!", ErrorCodes.PhysicalFileNotFound);
    public static ErrorMessage TechnicalSupport => new(HttpStatusCode.InternalServerError, "An unknown error occurred, contact the technical support!", ErrorCodes.TechnicalError);
}
