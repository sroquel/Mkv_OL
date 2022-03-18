using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MkvOL.Data.Entities
{
    public class CumplesEntity
    {
        public string Nombre { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM}")]
        public DateTime  Fecha { get; set; }

        public int Hoy { get; set; }
    }
}
