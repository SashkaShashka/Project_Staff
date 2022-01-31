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
using StaffWebApi.Exceptions;

namespace StaffWebApi.BL.Model
{
    public class StaffApiDto
    {
        public int ServiceNumber { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDay { get; set; }

        public IEnumerable<PostApiDto> Posts { get; set; }

        public decimal Salary => (decimal)Posts.Sum(p => (double)p.Position.Salary * p.Bet * (1 - Staff.NDFL));
        
        public StaffApiDto() { }
        public StaffApiDto(StaffDbDto staffDbDto)
        {
            ServiceNumber = staffDbDto.ServiceNumber;
            SurName = staffDbDto.SurName;
            FirstName = staffDbDto.FirstName;
            MiddleName = staffDbDto.MiddleName;
            BirthDay = staffDbDto.BirthDay;
            List<PostApiDto> posts = new List<PostApiDto>();
            if(staffDbDto.Positions != null)
                foreach (var post in staffDbDto.Positions)
                {
                    posts.Add(new PostApiDto(new PositionApiDto(post.Position), post.Bet));
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
                MiddleName = MiddleName,
                BirthDay = BirthDay,
                Positions = null
            };
        }
        public void Update(StaffDbDto staff)
        {
            staff.FirstName = FirstName;
            staff.MiddleName = MiddleName;
            staff.SurName = SurName;
            staff.BirthDay = BirthDay;
        }

        //public Exception AddPosition(StaffDbDto staff)
        //{
        //    List<StaffPositionDbDto> staffPositions = new List<StaffPositionDbDto>();
        //    HashSet<int> positions = new HashSet<int>();
        //    foreach (var post in staff.Positions)
        //    {
        //        positions.Add(post.PositionId);
        //        staffPositions.Add(post);
        //    }
        //    foreach (var post in Posts)
        //    {
        //        if (positions.Contains(post.Position.Id))
        //            return new AlreadyExistsException("Сотрудник уже имеет эту должность");
        //        var staffPosition = new StaffPositionDbDto()
        //        {
        //            StaffNumber = ServiceNumber,
        //            PositionId = post.Position.Id,
        //            Bet = post.Bet
        //        };
        //        staffPositions.Add(staffPosition);
        //    }
        //    staff.Positions = staffPositions;
        //    return null;
        //}
        //public Exception DeletePosition(StaffDbDto staff)
        //{
        //    List<StaffPositionDbDto> staffPositions = new List<StaffPositionDbDto>();
        //    HashSet<int> deletePositions = new HashSet<int>();
        //    HashSet<int> positions = new HashSet<int>();
        //    foreach (var post in Posts)
        //    {
        //        deletePositions.Add(post.Position.Id);
        //    }
        //    foreach (var post in staff.Positions)
        //    {
        //        positions.Add(post.PositionId);
        //    }
        //    foreach (var deletePosition in deletePositions)
        //    {
        //        if (!positions.Contains(deletePosition))
        //            return new KeyNotFoundException($"Cотрудник не имеет должность c ID ${deletePosition}, которую вы хотите удалить.");
        //    }
        //    foreach (var post in staff.Positions)
        //    {
        //        if(!deletePositions.Contains(post.PositionId))
        //            staffPositions.Add(post);
        //    }
        //    staff.Positions = staffPositions;
        //    return null;
        //}
        //public void UpdatePosition(StaffDbDto staff)
        //{
        //    List<StaffPositionDbDto> staffPositions = new List<StaffPositionDbDto>();
        //    foreach (var post in Posts)
        //    {
        //        var staffPosition = new StaffPositionDbDto()
        //        {
        //            StaffNumber = ServiceNumber,
        //            PositionId = post.Position.Id,
        //            Bet = post.Bet
        //        };
        //        staffPositions.Add(staffPosition);
        //    }
        //    staff.Positions = staffPositions;
        //}
    }
    public class StaffPositionsApiDto
    {
        public class Post
        {
            public int Position { get; set; }
            public int Bet { get; set; }
            public Post() { }
        }
        public int ServiceNumber { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public StaffPositionsApiDto() { }

        public void UpdatePosition(StaffDbDto staff)
        {
            List<StaffPositionDbDto> staffPositions = new List<StaffPositionDbDto>();
            foreach (var post in Posts)
            {
                var staffPosition = new StaffPositionDbDto()
                {
                    StaffNumber = ServiceNumber,
                    PositionId = post.Position,
                    Bet = post.Bet
                };
                staffPositions.Add(staffPosition);
            }
            staff.Positions = staffPositions;
        }

    }

}
