using Dapper;
using Infra.Data.DapperTypeHandlers;

namespace Infra.Extensions
{
    public static class DapperExtension
    {
        /// <summary>
        /// Método de extensão para registrar todos os TypeHandlers personalizados do Dapper.
        /// </summary>
        public static void AddDapperTypeHandlers()
        {
            // Registrar o TypeHandler para DateOnly
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        }
    }
}