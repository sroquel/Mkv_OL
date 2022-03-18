using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class EstadisticaAnualEntity
    {
        [Key]
        public int CoSucursal { get; set; }
        public bool Franquicia { get; set; }
        public bool Consignacion { get; set; }
        public string Nombre { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal ImporteAnt { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Importe { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal VarImp { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal ArticAnt { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Articulos { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal VarArtic { get; set; }
        public int TicketsAnt { get; set; }
        public int Tickets { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal VarTickets { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal TkProm { get; set; }
        public decimal TkPromAnt { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal VarTkProm { get; set; }
        public decimal AxTAnt { get; set; }
        public decimal AxT { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal VarAxT { get; set; }

    }
}
