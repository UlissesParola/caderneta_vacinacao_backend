using Core.Entities;
using Core.Interfaces.Repositories.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infra.Data.Repositories.Queries;

public class DependenteQueryRepository : IDependenteQueryRepository
{
    private readonly string _connectionString;

    public DependenteQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public async Task<Dependente> GetDependenteByIdAsync(string dependenteId)
    {
        using (var connection = Connection)
        {
            var query = "SELECT * FROM Dependentes WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Dependente>(query, new { Id = dependenteId });
        }
    }

    public async Task<IEnumerable<Dependente>> GetAllDependentesAsync()
    {
        using (var connection = Connection)
        {
            var query = "SELECT * FROM Dependentes";
            return await connection.QueryAsync<Dependente>(query);
        }
    }
}