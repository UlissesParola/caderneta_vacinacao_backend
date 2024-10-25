using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories.Commands;

public interface IDependenteCommandRepository
{
    Task<bool> CreateDependenteAsync(Dependente dependente);
    Task<bool> UpdateDependenteAsync(Dependente dependente);
    Task<bool> DeleteDependenteAsync(string dependenteId);
}
