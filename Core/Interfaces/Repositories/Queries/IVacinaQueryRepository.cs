using Core.Entities;

namespace Core.Interfaces.Repositories.Queries;

public interface IVacinaQueryRepository
{
    Task<Vacina> GetVacinaByIdAsync(string vacinaId);
    Task<IEnumerable<Vacina>> GetAllVacinasAsync();
}
