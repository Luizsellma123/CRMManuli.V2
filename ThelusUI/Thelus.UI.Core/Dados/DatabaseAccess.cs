using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Thelus.Core.Config;

namespace Thelus.Core.Dados
{
    public class DatabaseAccess
    {
        private SqlConnection CreateConnection()
        {
            var connectionString = ConfigurationManager.GetConnectionString();

            // Adicione esta trava de segurança:
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new Exception("ALerta: A ConnectionString retornou NULL ou VAZIA no ConfigurationManager!");
            }

            return new SqlConnection(connectionString);
        }

        #region SELECTs

        public async Task<List<T>> QueryAsync<T>(string sql, object param = null)
        {
            try
            {
                using var db = CreateConnection();
                await db.OpenAsync();

                var result = await db.QueryAsync<T>(sql, param);
                return result?.ToList() ?? new List<T>();
            }
            catch (System.Exception ex)
            {
                // Pega a exception real por dentro do Dapper ou do Driver
                var mensagemErro = ex.Message;
                var innerErro = ex.InnerException?.Message ?? "Sem InnerException";
                var stackTrace = ex.StackTrace;

                // Joga um erro limpo com todos os detalhes para vermos na tela de debug
                throw new System.Exception($"[ERRO DAPPER QUERY] {mensagemErro} | Inner: {innerErro}", ex);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        {
            using var db = CreateConnection();
            return await db.QueryFirstOrDefaultAsync<T>(sql, param);
        }

        #endregion

        #region INSERT / UPDATE / DELETE

        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            using var db = CreateConnection();
            return await db.ExecuteAsync(sql, param);
        }

        public async Task<TKey> ExecuteScalarAsync<TKey>(string sql, object param = null)
        {
            using var db = CreateConnection();
            return await db.ExecuteScalarAsync<TKey>(sql, param);
        }

        #endregion

        #region STORED PROCEDURES

        public async Task<List<T>> ExecuteProcedureQueryAsync<T>(string procedureName, object param = null)
        {
            using var db = CreateConnection();
            var result = await db.QueryAsync<T>(
                procedureName,
                param,
                commandType: CommandType.StoredProcedure
            );
            return result.ToList();
        }

        public async Task<int> ExecuteProcedureAsync(string procedureName, object param = null)
        {
            using var db = CreateConnection();
            return await db.ExecuteAsync(
                procedureName,
                param,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<DynamicParameters> ExecuteProcedureWithOutputAsync(string procedureName, DynamicParameters parameters)
        {
            using var db = CreateConnection();
            await db.ExecuteAsync(
                procedureName,
                parameters,
                commandType: CommandType.StoredProcedure
            );
            return parameters;
        }

        #endregion
    }
}