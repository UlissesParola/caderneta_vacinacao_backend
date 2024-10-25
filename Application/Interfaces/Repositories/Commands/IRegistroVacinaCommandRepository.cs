using Core.Entities;

namespace Core.Interfaces.Repositories.Commands;

public interface IRegistroVacinaCommandRepository
{
    Task<bool> CreateRegistroVacinaAsync(RegistroVacina registroVacina);
    Task<bool> UpdateRegistroVacinaAsync(RegistroVacina registroVacina);
    Task<bool> DeleteRegistroVacinaAsync(string registroVacinaId);
}
