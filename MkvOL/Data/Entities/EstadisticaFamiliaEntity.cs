using System.ComponentModel.DataAnnotations;

namespace MkvOL.Data.Entities
{
    public class EstadisticaFamiliaEntity
    {
        public int CoSucursal { get; set; }
        public string CoFamilia { get; set; }
        public string Familia { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Anio1 { get; set; }
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Anio2 { get; set; }
        public string UsuarioID { get; set; }
    }
}
