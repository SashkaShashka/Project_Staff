
using System;
using System.Collections.Generic;
using System.Text;

using StaffDBContext_Code_first.Model.DTO;

namespace Project_Staff.BD.Mapping
{
    static class PostMapper
    {
        public static Post Map(StaffPositionDbDto staffPosition)
        {
            if (staffPosition == null)
            {
                return null;
            }

            return new Post(PositionMapper.Map(staffPosition.Position), staffPosition.Bet);
        }

        public static StaffPositionDbDto Map(Post post, int staffNumber)
        {
            if (post == null)
            {
                return null;
            }

            return new StaffPositionDbDto()
            {
                StaffNumber = staffNumber,
                PositionId = post.Position.Id ?? 0,
                Bet = post.Bet
            };
        }
    }
}
