using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.Exceptions
{
    public class ConflictIdException : Exception
    {
        public ConflictIdException() : base("Не совпадают ID переданного объекта и ссылки") { }
    }
}
