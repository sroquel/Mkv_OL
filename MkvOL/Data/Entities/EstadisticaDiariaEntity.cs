using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class EstadisticaDiariaEntity
    {
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Fecha { get; set; }
        public string Dia { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Importe { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")] 
        public decimal Articulos { get; set; }
        public int Tickets { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal TkProm { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal ArtXTk { get; set; }
    }
}
