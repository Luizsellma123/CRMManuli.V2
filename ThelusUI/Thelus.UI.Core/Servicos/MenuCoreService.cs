using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thelus.Core.Config;
using Thelus.Core.Dados;
using Thelus.UI.Engine.Modelos;

namespace Thelus.Core.Servicos
{
    public class MenuCoreService : IMenuCoreService
    {
        private readonly DatabaseAccess _db;

        public MenuCoreService()
        {
            try
            {
                _db = new DatabaseAccess();
            }
            catch
            {
                _db = null;
            }
        }

        public async Task<List<MenuItem>> ObterMenusTabelaAsync()
        {
            try
            
            {
                string connStr = ConfigurationManager.GetConnectionString();

                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    const string sql = @"
                        SELECT 
                            IDMenu AS IdMenu, 
                            Nome AS Title, 
                            Endereco AS Url, 
                            IconeCSS AS Icon, 
                            TipoMenu,
                            CAST(0 AS BIT) AS IsTitle
                        FROM CRM_MENUS 
                        WHERE Ativo = 1 
                        ORDER BY Ordem";

                    var result = await _db.QueryAsync<MenuItem>(sql);

                    if (result != null && result.Count > 0)
                    {
                        return result;
                    }
                }
            }
            catch
            {
                // Fallback em caso de indisponibilidade de banco
            }

            return new List<MenuItem>();
        }
    }
}