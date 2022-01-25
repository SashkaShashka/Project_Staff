using System;
using System.Collections.Generic;

#nullable disable

namespace StaffDBContext.StaffDBContext
{
    public partial class VwPositionFilterByDivision
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Division { get; set; }
        public decimal Salary { get; set; }
    }
}
