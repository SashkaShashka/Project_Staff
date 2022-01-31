using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.Exceptions
{
    public class RelatedEntityException : Exception
    {
        RelatedEntityException() : base("Не совпадают ID переданного объекта и ссылки") { }
    }
}
