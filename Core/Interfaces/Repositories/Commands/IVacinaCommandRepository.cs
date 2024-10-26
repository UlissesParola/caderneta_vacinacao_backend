using Core.Entities;

namespace Core.Interfaces.Repositories.Commands;

public interface IVacinaCommandRepository
{
    Task<bool> CreateVacinaAsync(Vacina vacina);
    Task<bool> UpdateVacinaAsync(Vacina vacina);
    Task<bool> DeleteVacinaAsync(string vacinaId);
}
