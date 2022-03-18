using System;
using System.ComponentModel.DataAnnotations;

namespace MkvOL.Data.Entities
{
    public class EstadisticaVendedoraEntity
    {

        public int Semana { get; set; }
        public int CoVendedor { get; set; }
        public string Nombre { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Tickets { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Unidades { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal Importe { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}")]
        public decimal TkPromedio { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public decimal ArtXTk { get; set; }

    }
}
