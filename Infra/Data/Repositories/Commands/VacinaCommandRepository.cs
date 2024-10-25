using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Infra.Data.Context;

namespace Infra.Data.Repositories.Commands;

public class VacinaCommandRepository : IVacinaCommandRepository
{
    private readonly AppDbContext _context;

    public VacinaCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateVacinaAsync(Vacina vacina)
    {
        await _context.Vacinas.AddAsync(vacina);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateVacinaAsync(Vacina vacina)
    {
        _context.Vacinas.Update(vacina);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteVacinaAsync(string vacinaId)
    {
        var vacina = await _context.Vacinas.FindAsync(vacinaId);
        if (vacina == null) return false;

        _context.Vacinas.Remove(vacina);
        return await _context.SaveChangesAsync() > 0;
    }
}