using System;
using System.Collections.Generic;
using System.Text;

using StaffDBContext_Code_first.Model.DTO;

namespace Project_Staff.BD.Mapping
{
    static class StaffMapper
    {
        public static Staff Map(StaffDbDto staff)
        {
            if (staff == null)
            {
                return null;
            }

            return new Staff(staff.SurName, staff.FirstName, staff.MiddleName, staff.BirthDay)
            { ServiceNumber = staff.ServiceNumber };
        }

        public static StaffDbDto Map(Staff staff)
        {
            if (staff == null)
            {
                return null;
            }

            return new StaffDbDto()
            {
                ServiceNumber = staff.ServiceNumber,
                SurName = staff.SurName,
                FirstName = staff.FirstName,
                MiddleName = staff.MiddleName,
                BirthDay = staff.BirthDay
            };
        }
    }
}
