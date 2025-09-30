using Microsoft.EntityFrameworkCore;
using P1_AP1_JuanGuillen.DAL;
using P1_AP1_JuanGuillen.Models;
using System.Linq.Expressions;

namespace P1_AP1_JuanGuillen.Services;

public class RegistroServices(IDbContextFactory<Contexto> DbFactory)
{

    public async Task<List<ControlHuacal>> Listar(Expression<Func<ControlHuacal, bool >> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registros
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Insertar(ControlHuacal huacal)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Registros.Add(huacal);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(ControlHuacal huacal)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(huacal);
        return await contexto
            .SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int IdEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registros
            .Where(j => j.IdEntrada == IdEntrada)
            .ExecuteDeleteAsync() > 0;
    }

    public async Task<ControlHuacal?> Buscar(int Entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registros.FirstOrDefaultAsync(j => j.IdEntrada == Entrada);
    }

}

