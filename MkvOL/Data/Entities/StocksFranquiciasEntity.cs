using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class StocksFranquiciasEntity
    {
        [Key]
        public string Coarticulo { get; set; }
        public string CoBarra { get; set; }
        public decimal BBlanca { get; set; }
        public decimal Corrientes { get; set; }
        public decimal Jockey { get; set; }
        public decimal Jujuy { get; set; }
        public decimal Lomitas { get; set; }
        public decimal Toscas { get; set; }
        public decimal Lomas { get; set; }
        public decimal MDQ { get; set; }
        public decimal Martinez { get; set; }
        public decimal MzaPlaza { get; set; }
        public decimal Comahue { get; set; }
        public decimal Patagonia { get; set; }
        public decimal Palmares { get; set; }
        public decimal Quilmes { get; set; }
        public decimal Salta { get; set; }
        public decimal SanJuan { get; set; }
        public decimal TucuPortal { get; set; }
        public decimal TucuCentro { get; set; }
    }
}
