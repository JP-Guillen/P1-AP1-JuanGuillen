
using Microsoft.EntityFrameworkCore;
using P1_AP1_JuanGuillen.Models;

namespace P1_AP1_JuanGuillen.DAL;
public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto>options) : base(options) { }

    public DbSet<Registro>  Registros { get; set; }
}

