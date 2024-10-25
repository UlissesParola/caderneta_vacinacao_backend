using Core.Entities;
using Core.Interfaces.Repositories.Queries;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infra.Data.Repositories.Queries;

public class VacinaQueryRepository : IVacinaQueryRepository
{
    private readonly string _connectionString;

    public VacinaQueryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    private IDbConnection Connection => new NpgsqlConnection(_connectionString);

    public async Task<Vacina> GetVacinaByIdAsync(string vacinaId)
    {
        using (var connection = Connection)
        {
            var query = "SELECT * FROM Vacinas WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Vacina>(query, new { Id = vacinaId });
        }
    }

    public async Task<IEnumerable<Vacina>> GetAllVacinasAsync()
    {
        using (var connection = Connection)
        {
            var query = "SELECT * FROM Vacinas";
            return await connection.QueryAsync<Vacina>(query);
        }
    }
}