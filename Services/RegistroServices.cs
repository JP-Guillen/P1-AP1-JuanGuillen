using Microsoft.EntityFrameworkCore;
using P1_AP1_JuanGuillen.DAL;
using P1_AP1_JuanGuillen.Models;
using System.Linq.Expressions;

namespace P1_AP1_JuanGuillen.Services;

public class RegistroServices(IDbContextFactory<Contexto> DbFactory)
{

    public async Task<List<Registro>> Listar(Expression<Func<Registro, bool >> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registros
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}

