using System;
using System.Net;
using Transfer.WebApi.Exceptions;

namespace Transferor.Services.Orders
{
    public class ExceptionToResponseMapper : IExceptionToResponseMapper
    {
        public ExceptionResponse Map(Exception exception)
            => new ExceptionResponse(new {code = "error", message = exception.Message}, HttpStatusCode.BadRequest);
    }
}