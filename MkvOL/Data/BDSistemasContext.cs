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
    }
}
