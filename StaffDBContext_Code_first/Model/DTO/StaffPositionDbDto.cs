using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffDBContext_Code_first.Model.DTO
{
    [Table("StaffPosition")]
    public class StaffPositionDbDto
    {
        public int StaffNumber { get; set; }
        public int PositionId { get; set; }

        [Required]
        [Column(TypeName = "real")]
        public double Bet { get; set; }

        [ForeignKey("StaffNumber")]
        public StaffDbDto Staff { get; set; }

        public PositionDbDto Position { get; set; }
    }
}
