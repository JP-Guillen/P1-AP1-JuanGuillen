
using Microsoft.EntityFrameworkCore;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto>options) : base(options) { }

    public DbSet<Registro>  Registros { get; set; }
}

