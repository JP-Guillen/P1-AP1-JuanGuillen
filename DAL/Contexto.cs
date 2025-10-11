using Microsoft.EntityFrameworkCore;
using P1_AP1_JuanGuillen.Models;

namespace P1_AP1_JuanGuillen.DAL;
public class Contexto : DbContext
{
        public DbSet<EntradasHuacales> EntradaHuacal { get; set; }
        public DbSet<TiposHuacales> TipoHuacales { get; set; }
        public DbSet<EntradasHuacalesDetalle> HuacalDetalles { get; set; }
        public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TiposHuacales>().HasData(
             new List<TiposHuacales>()
             {
                new()
                {
                    TipoId = 1,
                    Descripcion = "Huacales verdes",
                },
                new()
                {
                    TipoId = 2,
                    Descripcion = "Huacales azules",
                },
                new()
                {
                    TipoId = 3,
                    Descripcion = "Huacales de huevos",
                },
             }
         );
        base.OnModelCreating(modelBuilder);
    } 
}

