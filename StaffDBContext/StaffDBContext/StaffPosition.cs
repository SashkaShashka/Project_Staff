using System;
using System.Collections.Generic;

#nullable disable

namespace StaffDBContext.StaffDBContext
{
    public partial class StaffPosition
    {
        public int StaffNumber { get; set; }
        public int PositionId { get; set; }
        public float Bet { get; set; }

        public virtual Position Position { get; set; }
        public virtual Staff StaffNumberNavigation { get; set; }
    }
}
