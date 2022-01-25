using System;
using System.Collections.Generic;

#nullable disable

namespace StaffDBContext.StaffDBContext
{
    public partial class VwStaffSearchByName
    {
        public int ServiceNumber { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
