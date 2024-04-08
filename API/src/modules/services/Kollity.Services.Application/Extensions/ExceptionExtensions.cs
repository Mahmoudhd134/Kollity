namespace Kollity.Services.Application.Extensions;

public static class ExceptionExtensions
{
    public static string GetErrorMessage(this Exception ex)
    {
        return ex.Message + (ex.InnerException != null
            ? "\nInner Exception => " + ex.InnerException.GetErrorMessage()
            : "");
    }
}