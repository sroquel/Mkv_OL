using System.ComponentModel.DataAnnotations;

namespace MkvOL.Data.Entities
{
    public class InternosEntity
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Debe Ingresar un {0}")]
        public int NroInterno { get; set; }

        [Required(ErrorMessage = "Debe Ingresar un {0}")]
        [StringLength(50, ErrorMessage = "El campo {0} debe tener entre {2} y {1} caracteres", MinimumLength = 3)]
        public string Usuario { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(20, ErrorMessage = "El campo {0} debe tener {1} caracteres como máximo")]
        public string Celular { get; set; }

        public virtual SectoresEntity Sector { get; set; }
    }
}
