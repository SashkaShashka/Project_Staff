using System;
using System.Collections.Generic;
using System.Text;

using StaffDBContext_Code_first.Model.DTO;

namespace Project_Staff.BD.Mapping
{
    static class PositionMapper
    {
        public static Position Map(PositionDbDto position)
        {
            if (position == null)
            {
                return null;
            }

            return new Position(position.Title, position.Division, position.Salary) { Id = position.Id };
        }

        public static void Update(this Position position, PositionDbDto positionDb)
        {
            positionDb.Title = position.Title;
            positionDb.Division = position.Devision;
            positionDb.Salary = position.Salary;
        }

        public static PositionDbDto Map(Position position)
        {
            if (position == null)
            {
                return null;
            }

            return new PositionDbDto()
            {
                Id = position.Id ?? 0,
                Title = position.Title,
                Division = position.Devision,
                Salary = position.Salary
            };
        }
    }
}
