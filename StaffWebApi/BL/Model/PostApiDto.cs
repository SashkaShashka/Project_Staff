using StaffDBContext_Code_first.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.BL.Model
{
    public class PostApiDto
    {
        public PositionApiDto Position { get; set; }
        public double Bet { get; set; }
        public PostApiDto() {}
        public PostApiDto(PositionApiDto staffPosition, double bet)
        {
            Position = new PositionApiDto(staffPosition);
            Bet = bet;
        }
    }
}
