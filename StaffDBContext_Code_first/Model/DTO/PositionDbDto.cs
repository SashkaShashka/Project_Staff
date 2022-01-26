using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffDBContext_Code_first.Model.DTO
{
    [Table("Position")]
    public class PositionDbDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        [MaxLength(200)]
        public string Division { get; set; }

        [Required]
        [Column(TypeName = "decimal")]
        public decimal Salary { get; set; }
    }
}
