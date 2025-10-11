using Microsoft.EntityFrameworkCore;
using P1_AP1_JuanGuillen.DAL;
using P1_AP1_JuanGuillen.Models;
using System.Linq.Expressions;
using static P1_AP1_JuanGuillen.Services.RegistroServices;

namespace P1_AP1_JuanGuillen.Services;

public class RegistroServices
{
    private readonly IDbContextFactory<Contexto> DbFactory;

    public RegistroServices(IDbContextFactory<Contexto> dbFactory)
    {
        DbFactory = dbFactory;
    }

    public async Task<List<EntradasHuacales>> Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradaHuacal
            .Include(e => e.EntradasHuacalesDetalles)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<EntradasHuacales?> Buscar(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradaHuacal
            .Include(e => e.EntradasHuacalesDetalles)
                .ThenInclude(d => d.TipoHuacal)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.IdEntrada == idEntrada);
    }

    public async Task<bool> Insertar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.EntradaHuacal.Add(entrada);
        await AfectarExistencia(entrada.EntradasHuacalesDetalles.ToArray(), TipoOperacion.Suma);
        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(EntradasHuacales entrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

      
        var anterior = await contexto.EntradaHuacal
            .Include(e => e.EntradasHuacalesDetalles)
            .FirstOrDefaultAsync(e => e.IdEntrada == entrada.IdEntrada);

        if (anterior == null)
            return false;

      
        await AfectarExistencia(anterior.EntradasHuacalesDetalles.ToArray(), TipoOperacion.Resta);
        await AfectarExistencia(entrada.EntradasHuacalesDetalles.ToArray(), TipoOperacion.Suma);

        foreach (var detAnterior in anterior.EntradasHuacalesDetalles.ToList())
        {
            if (!entrada.EntradasHuacalesDetalles.Any(d => d.DetallesId == detAnterior.DetallesId))
            {
                contexto.Remove(detAnterior);
            }
        }

        
        foreach (var detNuevo in entrada.EntradasHuacalesDetalles)
        {
            if (detNuevo.DetallesId == 0)
            {
              
                contexto.Add(detNuevo);
            }
            else
            {
              
                var detExistente = anterior.EntradasHuacalesDetalles
                    .First(d => d.DetallesId == detNuevo.DetallesId);

                detExistente.TipoId = detNuevo.TipoId;
                detExistente.Cantidad = detNuevo.Cantidad;
                detExistente.Precio = detNuevo.Precio;
            }
        }

   
        anterior.NombreCliente = entrada.NombreCliente;
        anterior.fecha = entrada.fecha;
        anterior.cantidad = entrada.cantidad;
        anterior.precio = entrada.precio;

        return await contexto.SaveChangesAsync() > 0;
    }



    public async Task<bool> Eliminar(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var entrada = await contexto.EntradaHuacal
            .Include(e => e.EntradasHuacalesDetalles)
            .FirstOrDefaultAsync(e => e.IdEntrada == idEntrada);

        if (entrada == null)
            return false;

        await AfectarExistencia(entrada.EntradasHuacalesDetalles.ToArray(), TipoOperacion.Resta);

        contexto.HuacalDetalles.RemoveRange(entrada.EntradasHuacalesDetalles);
        contexto.EntradaHuacal.Remove(entrada);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Existe(int idEntrada)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradaHuacal
            .AnyAsync(e => e.IdEntrada == idEntrada);
    }

    public async Task<bool> Guardar(EntradasHuacales entrada)
    {
        if (!await Existe(entrada.IdEntrada))
            return await Insertar(entrada);
        else
            return await Modificar(entrada);
    }

    public async Task AfectarExistencia(EntradasHuacalesDetalle[] detalles, TipoOperacion tipo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        foreach (var det in detalles)
        {
            var tipoHuacal = await contexto.TipoHuacales
                .SingleAsync(t => t.TipoId == det.TipoId);

            if (tipo == TipoOperacion.Suma)
            {
                tipoHuacal.Existencia += det.Cantidad;
            }
            else if (tipo == TipoOperacion.Resta)
            {
                tipoHuacal.Existencia -= det.Cantidad;
            }
        }

        await contexto.SaveChangesAsync();
    }

    public async Task<List<TiposHuacales>> ListarTipos()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TipoHuacales.AsNoTracking().ToListAsync();
    }

    public enum TipoOperacion
    {
        Suma,
        Resta
    }
}
