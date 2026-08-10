using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;
using Thelus.UI.Engine.Servicos;

namespace Thelus.Core.Servicos
{
    public class EntityServiceResolver
    {
        private readonly IEnumerable<IEntityService> _servicosEspecificos;
        private readonly IGenericEntityService _genericService;

        public EntityServiceResolver(
            IEnumerable<IEntityService> servicosEspecificos,
            IGenericEntityService genericService)
        {
            _servicosEspecificos = servicosEspecificos;
            _genericService = genericService;
        }

        // 1. CONSULTA LISTAGEM
        public async Task<List<dynamic>> ObterListagemAsync(string entityName, FiltroConsulta filtro = null)
        {
            filtro ??= new FiltroConsulta { EntityName = entityName };

            // Procura serviço customizado para essa entidade (ex: UsuarioServico)
            var servicoEspecifico = _servicosEspecificos
                .FirstOrDefault(s => s.EntityName.Equals(entityName, StringComparison.OrdinalIgnoreCase));

            if (servicoEspecifico != null)
            {
                return await servicoEspecifico.ObterListagemAsync(filtro);
            }

            // Fallback: consulta genérica
            return await _genericService.ObterDadosGenericosAsync(filtro);
        }

        // 2. CONSULTA POR ID
        public async Task<object> ObterPorIdAsync(string entityName, int id)
        {
            // Tenta obter pelo serviço específico
            var servicoEspecifico = _servicosEspecificos
                .FirstOrDefault(s => s.EntityName.Equals(entityName, StringComparison.OrdinalIgnoreCase));

            if (servicoEspecifico != null)
            {
                return await servicoEspecifico.ObterPorIdAsync(id);
            }

            // Fallback: busca direto via serviço genérico de banco
            return await _genericService.ObterPorIdGenericoAsync(entityName, id);
        }

        // 3. GRAVAÇÃO / SALVAMENTO
        public async Task<ResultadoOperacao> SalvarAsync(string entityName, object item)
        {
            if (item == null)
            {
                return ResultadoOperacao.Falha("Nenhum objeto foi fornecido para a gravação.");
            }

            // Procura serviço customizado da entidade
            var servicoEspecifico = _servicosEspecificos
                .FirstOrDefault(s => s.EntityName.Equals(entityName, StringComparison.OrdinalIgnoreCase));

            if (servicoEspecifico != null)
            {
                return await servicoEspecifico.SalvarAsync(item);
            }

            // Fallback: realização da gravação genérica
            return await _genericService.SalvarGenericoAsync(entityName, item);
        }
    }
}