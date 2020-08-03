using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class StockEcommerceEntity
    {
        [Key]
        public string CoArticulo { get; set; }
        public string CoBarra { get; set; }
        public string Descripcion { get; set; }
        [DisplayFormat(DataFormatString = "{0:#}")]
        public decimal Stock { get; set; }
    }
}
