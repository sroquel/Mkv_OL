using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
   
    public class DespachosEntity
    {
        [Display(Name = "Nro Pedido")]
        [DisplayFormat(DataFormatString = "{0:G}")]
        public decimal NoPedido { get; set; }
  
        public string Seguimiento { get; set; }
        public string Empresa { get; set; }
        [Display(Name = "Modo de Envío")]
        public string ModoEnvio { get; set; }
        [Display(Name = "Fecha Despacho")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaDespacho { get; set; }
        public string Observaciones { get; set; }
    }
}
