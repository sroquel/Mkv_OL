using Microsoft.EntityFrameworkCore;
using MkvOL.Data.Entities;

namespace MkvOL.Data
{
    public class BDSistemasContext : DbContext
    {


        public BDSistemasContext(DbContextOptions<BDSistemasContext> options) : base(options)
        {

        }

        public DbSet<InternosEntity> Internos { get; set; }
        public DbSet<StocksLocalesEntity> StockLocales { get; set; }
        public DbSet<StocksFranquiciasEntity> StocksFranquicias { get; set; }
        public DbSet<VentaDiariaEntity> VentaDiaria { get; set; }
        public DbSet<DashVentaEntity> DashVenta { get; set; }
        public DbSet<DashVentaEntity> DashVentaEntity { get; set; }
        public DbSet<StockEcommerceEntity> StockEcommerceEntity { get; set; }
        public DbSet<DespachosEntity> Despachos { get; set; }
        public DbSet<EstadisticaAnualEntity> EstadisticaAnual { get; set; }
        public DbSet<EstadAnualSucursalEntity> EstadAnualSucursal { get; set; }
        public DbSet<EstadisticaDiariaEntity> EstadisticaDiaria { get; set; }
        public DbSet<EstadisticaVendedoraEntity> EstadisticaVendedora { get; set; }
        public DbSet<DatosDepositosEntity> DatosDepositos { get; set; }
        public DbSet<CumplesEntity> Cumples { get; set; }
        public DbSet<EstadisticaFamiliaEntity> EstadisticaFamilia { get; set; }
        public DbSet<EstadisticaArticuloEntity> EstadisticaArticulo { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DespachosEntity>(entity =>
                {
                    entity.HasNoKey();
                });
            modelBuilder.Entity<EstadisticaDiariaEntity>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<CumplesEntity>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<EstadisticaVendedoraEntity>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<EstadisticaFamiliaEntity>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<EstadisticaArticuloEntity>(entity =>
            {
                entity.HasNoKey();
            });


        }




    }
}
