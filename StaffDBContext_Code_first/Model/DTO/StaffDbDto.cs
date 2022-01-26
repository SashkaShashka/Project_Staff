using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffDBContext_Code_first.Model.DTO
{
    [Table("Staff")]
    public class StaffDbDto
    {
        [Key]
        public int ServiceNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string SurName { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string MiddleName { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        public ICollection<StaffPositionDbDto> Positions { get; set; }
    }
}
