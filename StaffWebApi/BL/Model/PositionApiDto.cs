using StaffDBContext_Code_first.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.BL.Model
{
    public class PositionApiDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Division { get; set; }
        public decimal Salary { get; set; }
        public PositionApiDto() { }
        
        public PositionDbDto Create()
        {
            return new PositionDbDto()
            {
                Id = Id,
                Title = Title,
                Division = Division,
                Salary = Salary
            };
        }
        public void Update(PositionDbDto position)
        {
            position.Id = Id;
            position.Title = Title;
            position.Division = Division;
            position.Salary = Salary;
        }
        public PositionApiDto(PositionDbDto position)
        {
            Id = position.Id;
            Title = position.Title;
            Division = position.Division;
            Salary = position.Salary;
        }
        public PositionApiDto(PositionApiDto position)
        {
            Id = position.Id;
            Title = position.Title;
            Division = position.Division;
            Salary = position.Salary;
        }
    }
}
