using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class DashVentaEntity
    {
        [Key]
        public int CoSucursal { get; set; }
        public string Sucursal { get; set; }
        public bool Franquicia { get; set; }

        [DataType(DataType.Currency)]
        public bool Consignacion { get; set; }

        [DataType(DataType.Currency)]
        public decimal Pronostico { get; set;}

        [Display(Name = "Venta Dia")]
        [DataType(DataType.Currency)]
        public decimal VentaDia { get; set; }

        [Display(Name = "Venta Año Ant")]
        [DataType(DataType.Currency)]
        public decimal VentaAnt { get; set; }
        
        [Display(Name = "Pron. Acumulado")]
        [DataType(DataType.Currency)]
        public decimal PronAcum { get; set; }

        [DataType(DataType.Currency)]
        public decimal Acumulado { get; set; }

        [Display(Name = "Acum Año Ant")]
        [DataType(DataType.Currency)]
        public decimal AcumAnt { get; set; }


    }
}
