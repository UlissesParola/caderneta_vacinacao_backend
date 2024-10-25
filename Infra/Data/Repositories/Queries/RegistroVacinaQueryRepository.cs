using Core.Entities;
using Core.Interfaces.Repositories.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infra.Data.Repositories.Queries;

public class RegistroVacinaQueryRepository : IRegistroVacinaQueryRepository
{
    private readonly string _connectionString;

    public RegistroVacinaQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public async Task<RegistroVacina> GetRegistroVacinaByIdAsync(string registroVacinaId)
    {
        using (var connection = Connection)
        {
            var query = "SELECT * FROM RegistrosVacinas WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<RegistroVacina>(query, new { Id = registroVacinaId });
        }
    }

    public async Task<IEnumerable<RegistroVacina>> GetRegistrosVacinasByDependenteIdAsync(string dependenteId)
    {
        using (var connection = Connection)
        {
            var query = "SELECT * FROM RegistrosVacinas WHERE DependenteId = @DependenteId";
            return await connection.QueryAsync<RegistroVacina>(query, new { DependenteId = dependenteId });
        }
    }
}