using System.Collections.Generic;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;

namespace Thelus.Core.Servicos
{
    public interface IGenericEntityService
    {
        Task<List<dynamic>> ObterDadosGenericosAsync(FiltroConsulta filtro);

        // Busca genérica por ID
        Task<dynamic> ObterPorIdGenericoAsync(string entityName, int id);

        // Contrato para gravação genérica por tabela/entidade
        Task<ResultadoOperacao> SalvarGenericoAsync(string entityName, object item);
    }
}