using System.Collections.Generic;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;

namespace Thelus.Core.Servicos
{
    public interface IEntityService
    {
        // Define o nome da entidade que o serviço atende (ex: "usuario", "cliente")
        string EntityName { get; }

        Task<List<dynamic>> ObterListagemAsync(FiltroConsulta filtro = null);

        // Busca um registro específico por ID
        Task<dynamic> ObterPorIdAsync(int id);

        // Contrato para salvar instâncias tipadas específicas
        Task<ResultadoOperacao> SalvarAsync(object item);
    }
}