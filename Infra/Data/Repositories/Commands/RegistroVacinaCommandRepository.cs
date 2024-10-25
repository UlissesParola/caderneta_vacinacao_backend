using Core.Entities;
using Core.Interfaces.Repositories.Commands;
using Infra.Data.Context;

namespace Infra.Data.Repositories.Commands;

public class RegistroVacinaCommandRepository : IRegistroVacinaCommandRepository
{
    private readonly AppDbContext _context;

    public RegistroVacinaCommandRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateRegistroVacinaAsync(RegistroVacina registroVacina)
    {
        await _context.RegistrosVacinas.AddAsync(registroVacina);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateRegistroVacinaAsync(RegistroVacina registroVacina)
    {
        _context.RegistrosVacinas.Update(registroVacina);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteRegistroVacinaAsync(string registroVacinaId)
    {
        var registroVacina = await _context.RegistrosVacinas.FindAsync(registroVacinaId);
        if (registroVacina == null) return false;

        _context.RegistrosVacinas.Remove(registroVacina);
        return await _context.SaveChangesAsync() > 0;
    }
}