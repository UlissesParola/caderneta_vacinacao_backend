using Dapper;
using System.Data;

namespace Infra.Data.DapperTypeHandlers;

public class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
{
    public override void SetValue(IDbDataParameter parameter, DateOnly value)
    {
        // Define o valor como DateTime para o banco de dados
        parameter.Value = value.ToDateTime(TimeOnly.MinValue);
    }

    public override DateOnly Parse(object value)
    {
        // Converte o valor do banco de dados para DateOnly
        return DateOnly.FromDateTime(Convert.ToDateTime(value));
    }
}