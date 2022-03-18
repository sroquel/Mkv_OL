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

        public bool Consignacion { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Pronostico { get; set;}

        [Display(Name = "Venta Dia")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal VentaDia { get; set; }

        [Display(Name = "% Cumpl")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Cumplim { get; set; }

        [Display(Name = "Venta Año Ant")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal VentaAnt { get; set; }
        
        [Display(Name = "Pron. Acumulado")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal PronAcum { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Acumulado { get; set; }

        [Display(Name = "% Cumpl Acum")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal CumplimAcum { get; set; }

        [Display(Name = "Acum Año Ant")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal AcumAnt { get; set; }


    }
}
