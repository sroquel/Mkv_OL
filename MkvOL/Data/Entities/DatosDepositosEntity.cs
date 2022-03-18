using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class DatosDepositosEntity
    {
        [Key]
        public int CoSucursal { get; set; }
        public string Nombre{ get; set; }
        public bool Activo { get; set; }
        public bool Propio { get; set; }
        public bool Franquicia { get; set; }
        public string Base { get; set; }
    }
}
