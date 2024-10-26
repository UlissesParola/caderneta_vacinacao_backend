using Core.Entities;

namespace Core.Interfaces.Repositories.Queries;

public interface IRegistroVacinaQueryRepository
{
    Task<RegistroVacina> GetRegistroVacinaByIdAsync(string registroVacinaId);
    Task<IEnumerable<RegistroVacina>> GetRegistrosVacinasByDependenteIdAsync(string dependenteId);
}
