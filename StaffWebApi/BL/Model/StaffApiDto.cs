using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StaffDBContext_Code_first.Model.DTO;
using Project_Staff;

namespace StaffWebApi.BL.Model
{
    public class StaffApiDto
    {
        public int ServiceNumber { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDay { get; set; }

        public IEnumerable<PostApiDto>? Posts { get; set; }

        public decimal Salary => (decimal)Posts.Sum(p => (double)p.Position.Salary * p.Bet * (1 - Staff.NDFL));
        
        public StaffApiDto(StaffDbDto staffDbDto, IEnumerable<PositionDbDto> positionsStaff=null)
        {
            ServiceNumber = staffDbDto.ServiceNumber;
            SurName = staffDbDto.SurName;
            FirstName = staffDbDto.FirstName;
            MiddleName = staffDbDto.MiddleName;
            BirthDay = staffDbDto.BirthDay;
            List<PostApiDto> posts = new List<PostApiDto>();
            if (positionsStaff!=null)
            {
                foreach (var post in staffDbDto.Positions)
                {
                    var help = new PostApiDto(positionsStaff.FirstOrDefault(p => p.Id == post.PositionId), post.Bet);
                    posts.Add(help);
                }
            }
            Posts = posts;
        }

        public StaffDbDto Create()
        {
            return new StaffDbDto()
            {
                ServiceNumber = ServiceNumber,
                FirstName = FirstName,
                SurName = SurName,
                MiddleName = MiddleName
            };
        }

    }
}
