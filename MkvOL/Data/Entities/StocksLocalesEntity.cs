using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class StocksLocalesEntity
    {
        [Key]
        public string Coarticulo { get; set; }
        public string CoBarra { get; set; }
        public decimal Minorista { get; set; }
        public decimal Mayo { get; set; }
        public decimal Franq { get; set; }
        public decimal Corte { get; set; }
        public decimal Intermedio { get; set; }
        public decimal Abasto { get; set; }
        public decimal Solar { get; set; }
        public decimal Poeste { get; set; }
        public decimal VCrespo { get; set; }
        public decimal Unicenter { get; set; }
        public decimal Caballito { get; set; }
        public decimal Micro { get; set; }
        public decimal Pacifico { get; set; }
        public decimal Soleil { get; set; }
        public decimal AltoP { get; set; }
        public decimal Avellaneda { get; set; }
        public decimal SanJusto { get; set; }
        public decimal Dot { get; set; }
        public decimal Devoto { get; set; }
        public decimal Palmas { get; set; }
        public decimal NvoCentro { get; set; }
        public decimal CbaShop { get; set; }
        public decimal Recoleta { get; set; }
        public decimal Rosario { get; set; }

    }
}
