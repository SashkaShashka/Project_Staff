using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.Exceptions
{
    public static class CheckException
    {
        public static (int,string) CheckError(Exception ex)
        {
            if (ex == null)
            {
                return (200,null);
            }
            if (ex is ArgumentException)
            {
                return (400,ex.Message);
            }
            if (ex is KeyNotFoundException)
            {
                return (404, ex.Message);
            }
            if (ex is ConflictIdException)
            {
                return (409, ex.Message);
            }
            if (ex is AlreadyExistsException)
            {
                return (409, ex.Message);
            }
            return (409, null);

        }
    }

}
