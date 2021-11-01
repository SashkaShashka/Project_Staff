using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Project_Staff
{
    [XmlType(TypeName = "Staffs")]
    public class StaffFileDto
    {
    [XmlAttribute("staff")]


        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDay { get; set; }
        public PostFileDto[] Posts { get; set; }

        public static StaffFileDto Map(Staff staff)
        {
            return new StaffFileDto()
            {
                SurName = staff.SurName,
                FirstName = staff.FirstName,
                MiddleName = staff.MiddleName,
                BirthDay = staff.BirthDay,
                Posts = staff.Posts.Select(post => PostFileDto.Map(post)).ToArray(),
            };
        }
        public static Staff Map(StaffFileDto staff)
        {
            return new Staff(
                staff.SurName,
                staff.FirstName,
                staff.MiddleName,
                staff.BirthDay,
                staff.Posts.Select(post=>PostFileDto.Map(post)).ToList()
            );
        }
    }
}
