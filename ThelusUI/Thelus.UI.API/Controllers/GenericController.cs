using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Thelus.Core.Servicos;
using Thelus.UI.Engine.Modelos;
using Thelus.UI.Engine.Servicos;

namespace Thelus.UI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController : ControllerBase
    {
        private readonly EntityServiceResolver _serviceResolver;

        public GenericController(EntityServiceResolver serviceResolver)
        {
            _serviceResolver = serviceResolver;
        }

        // Rota: GET /api/generic/usuarios
        [HttpGet("{entityName}")]
        public async Task<IActionResult> ObterListagem(string entityName)
        {
            try
            {
                var dados = await _serviceResolver.ObterListagemAsync(entityName);
                return Ok(dados);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        // Rota: GET /api/generic/usuarios/5 (Busca por ID)
        [HttpGet("{entityName}/{id:int}")]
        public async Task<IActionResult> ObterPorId(string entityName, int id)
        {
            try
            {
                var dado = await _serviceResolver.ObterPorIdAsync(entityName, id);
                if (dado == null) return NotFound(new { mensagem = "Registro não encontrado." });

                return Ok(dado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        // Rota: POST /api/generic/usuarios (Salvar / Inserir / Atualizar)
        [HttpPost("{entityName}")]
        public async Task<IActionResult> Salvar(string entityName, [FromBody] JsonElement body)
        {
            try
            {
                // 1. Descobre o Type C# registrado para essa entidade (ex: typeof(UsuarioTeste))
                var entityType = EntityRegistry.GetEntityType(entityName);
                object objetoInstanciado = body;

                // 2. Converte o JSON genérico na instância C# fortemente tipada
                if (entityType != null)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    objetoInstanciado = JsonSerializer.Deserialize(body.GetRawText(), entityType, options);
                }

                // 3. O Resolver e o UsuarioServico agora devolvem a instância do ResultadoOperacao
                ResultadoOperacao resultado = await _serviceResolver.SalvarAsync(entityName, objetoInstanciado);

                // 4. Se a regra de negócio/banco falhar, retorna HTTP 400 com a mensagem específica do serviço
                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado);
                }

                // 5. Em caso de sucesso, retorna HTTP 200 com o objeto e a mensagem de sucesso
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ResultadoOperacao.Falha($"Erro interno ao processar requisição: {ex.Message}"));
            }
        }
    }
}