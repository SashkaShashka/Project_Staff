using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.Exceptions
{
    public static class ExceptionActionResultExtension
    {
        public static ActionResult GetResultObject(this Exception exception, string errorMsg = null, object result = null)
        {
            if (exception is SaveChangesException)
            {
                return new BadRequestObjectResult(errorMsg);
            }
            else if (exception is KeyNotFoundException)
            {
                return new NotFoundObjectResult(errorMsg);
            }
            else if (exception is AlreadyExistsException)
            {
                return new ConflictObjectResult(errorMsg);
            }
            else if (exception != null)
            {
                return new StatusCodeResult(500);
            }
            else
            {
                return new OkObjectResult(result);
            }
        }
    }
}
