using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class VentaDiariaEntity
    {
        [Key]
        public int CoSucursal { get; set; }
        public string Sucursal { get; set; }
        [DataType(DataType.Currency)]
        public decimal Importe { get; set; }
    }
}
