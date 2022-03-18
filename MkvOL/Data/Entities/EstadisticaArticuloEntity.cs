using System.ComponentModel.DataAnnotations;

namespace MkvOL.Data.Entities
{
    public class EstadisticaArticuloEntity
    {
        public string CoArticulo { get; set; }
        public string CoBarra { get; set; }
        public string Descripcion { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Anio1 { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Anio2 { get; set; }
        public string UsuarioID { get; set; }

    }
}
