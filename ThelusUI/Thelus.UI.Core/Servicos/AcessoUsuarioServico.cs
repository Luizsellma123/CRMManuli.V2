using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thelus.Core.Config;
using Thelus.Core.Dados;
using Thelus.UI.Engine.Modelos;

namespace Thelus.Core.Servicos
{
    public class AcessoUsuarioServico : IEntityService
    {
        private readonly DatabaseAccess _db;
        private readonly ISessaoUsuario _sessaoUsuario;

        public string EntityName => "acesso";

        public AcessoUsuarioServico(ISessaoUsuario sessaoUsuario)
        {
            _sessaoUsuario = sessaoUsuario;
            try { _db = new DatabaseAccess(); } catch { _db = null; }
        }

        public async Task<List<dynamic>> ObterListagemAsync(FiltroConsulta filtro = null)
        {
            string recurso = filtro?.EntityName?.ToLower() ?? "";
            string usuarioLogado = _sessaoUsuario?.ObterCodigoUsuario() ?? "LUIZ.CARLOS";

            string sql = recurso switch
            {
                "acesso-empresas" => @"
            SELECT 
                E.IDEmpresa AS Id,
                E.NomeEmpresa AS Descricao
            FROM CRM_CADASTRO_USUARIO U
            INNER JOIN CRM_CADASTRO_USUARIO_EMPRESA UE 
                ON U.IDUsuario = UE.IDUsuario
            INNER JOIN CRM_EMPRESA_FILIAL E 
                ON UE.IDEmpresa = E.IDEmpresa
            WHERE U.CodigoUsuario = @UsuarioLogado
            ORDER BY E.NomeEmpresa",

                "acesso-depositos" => @"
            SELECT 
                d.IdDeposito   AS Id, 
                d.NomeDeposito AS Descricao 
            FROM CRM_DEPOSITOS d
            INNER JOIN CRM_USUARIO_DEPOSITO ud ON d.IdDeposito = ud.IdDeposito
            WHERE ud.UsuCod = @UsuarioLogado AND d.Ativo = 1 
            ORDER BY d.NomeDeposito",

                _ => null
            };

            if (string.IsNullOrEmpty(sql)) return new List<dynamic>();

            try
            {
                if (_db != null)
                {
                    var dados = await _db.QueryAsync<dynamic>(sql, new { UsuarioLogado = usuarioLogado });
                    if (dados != null && dados.Count > 0) return dados;
                }
            }
            catch (Exception ex)
            {
                // Logar ex (opcional) para não deixar falhas de banco silenciosas
            }

            // Fallback de Contingência / Desenvolvimento
            return recurso switch
            {
                "acesso-empresas" => new List<dynamic> { new { Id = 1, Descricao = "Manupackaging Fitasa" } },
                "acesso-depositos" => new List<dynamic> { new { Id = 1, Descricao = "Depósito Central" } },
                _ => new List<dynamic>()
            };
        }

        public async Task<object> ObterPorIdAsync(int id) => null;
        public async Task<ResultadoOperacao> SalvarAsync(object item) => ResultadoOperacao.Falha("Operação não permitida.");
    }
}