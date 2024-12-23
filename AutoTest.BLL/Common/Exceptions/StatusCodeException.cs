using System.Net;

namespace AutoTest.BLL.Common.Exceptions;

public class StatusCodeException(HttpStatusCode code, string message) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = code;
}
