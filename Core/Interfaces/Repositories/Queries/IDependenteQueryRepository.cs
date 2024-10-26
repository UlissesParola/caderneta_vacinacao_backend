using Core.Entities;

namespace Core.Interfaces.Repositories.Queries;

public interface IDependenteQueryRepository
{
    Task<Dependente> GetDependenteByIdAsync(string dependenteId);
    Task<IEnumerable<Dependente>> GetAllDependentesAsync();
}
