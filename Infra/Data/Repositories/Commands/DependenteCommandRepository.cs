using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Infra.Data.Context;

namespace Infra.Data.Repositories.Commands;

public class DependenteCommandRepository : IDependenteCommandRepository
{
    private readonly AppDbContext _context;

    public DependenteCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateDependenteAsync(Dependente dependente)
    {
        await _context.Dependentes.AddAsync(dependente);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateDependenteAsync(Dependente dependente)
    {
        _context.Dependentes.Update(dependente);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteDependenteAsync(string dependenteId)
    {
        var dependente = await _context.Dependentes.FindAsync(dependenteId);
        if (dependente == null) return false;

        _context.Dependentes.Remove(dependente);
        return await _context.SaveChangesAsync() > 0;
    }
}