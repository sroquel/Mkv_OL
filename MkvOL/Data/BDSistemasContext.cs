using Microsoft.EntityFrameworkCore;
using MkvOL.Data.Entities;

namespace MkvOL.Data
{
    public class BDSistemasContext: DbContext
    {
        public BDSistemasContext(DbContextOptions<BDSistemasContext> options) : base(options)
        {

        }

        public DbSet<InternosEntity> Internos { get; set; }
        public DbSet<SectoresEntity> Sectores { get; set; }
        public DbSet<StocksLocalesEntity> StockLocales { get; set; }
        public DbSet<VentaDiariaEntity> VentaDiaria { get; set; }
        public DbSet<DashVentaEntity> DashVenta { get; set; }
        public DbSet<DashVentaEntity> DashVentaEntity { get; set; }
        public DbSet<StockEcommerceEntity> StockEcommerceEntity { get; set; }
 
    }
}
