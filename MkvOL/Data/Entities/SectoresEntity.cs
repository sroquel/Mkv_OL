using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class SectoresEntity
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Debe Ingresar un {0}")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Sector { get; set; }

        public virtual ICollection<InternosEntity> Internos { get; set; }
    }
}
