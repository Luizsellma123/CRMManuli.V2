using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Thelus.Core.Config;
using Thelus.Core.Dados;
using Thelus.UI.Engine.Modelos; // Importante para utilizar a classe ResultadoOperacao
using Thelus.UI.Model;
using Thelus.UI.Model.Entidades;

namespace Thelus.Core.Servicos
{
    public class UsuarioServico : IEntityService
    {
        private readonly DatabaseAccess _db;

        public UsuarioServico()
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

        // Identificador da entidade aceita por este serviço
        public string EntityName => "usuarios";

        // 1. LISTAR REGISTROS
        public async Task<List<dynamic>> ObterListagemAsync(FiltroConsulta filtro = null)
        {
            try
            {
                string connStr = ConfigurationManager.GetConnectionString();

                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    string sql = @"
                        SELECT 
                            u.IdUsuario         AS IdUsuario,
                            u.Codigo            AS Codigo,
                            u.Nome              AS Nome,
                            u.Cpf               AS Cpf,
                            u.Cadastro          AS Cadastro,
                            u.Email             AS Email,
                            u.Telefone          AS Telefone,
                            u.Celular           AS Celular,
                            u.Senha             AS Senha,
                            u.IdTabela          AS IdTabela,
                            u.IdStatus          AS IdStatus,
                            u.UsuarioInclusao   AS UsuarioInclusao,
                            u.DataInclusao      AS DataInclusao,
                            u.UsuarioAlteracao  AS UsuarioAlteracao,
                            u.DataAlteracao     AS DataAlteracao
                        FROM Usuarios u
                        INNER JOIN [STATUS] s ON s.IdStatus = u.IdStatus AND s.IDTabela = 33";

                    List<UsuarioTeste> dados = await _db.QueryAsync<UsuarioTeste>(sql);

                    if (dados != null && dados.Count > 0)
                    {
                        return dados.Cast<dynamic>().ToList();
                    }
                }
            }
            catch
            {
                // Fallback em caso de indisponibilidade de banco
            }

            // FALLBACK / MOCK
            var mockList = new List<UsuarioTeste>
            {
                new UsuarioTeste { IdUsuario = 1, Codigo = "admin", Nome = "Administrador do Sistema", Email = "admin@thelus.com.br", IdStatus = 1 },
                new UsuarioTeste { IdUsuario = 2, Codigo = "operador", Nome = "João da Silva", Email = "joao@thelus.com.br", IdStatus = 1 },
                new UsuarioTeste { IdUsuario = 3, Codigo = "suporte", Nome = "Maria Santos", Email = "maria@thelus.com.br", IdStatus = 2 }
            };

            return mockList.Cast<dynamic>().ToList();
        }

        // 2. OBTER REGISTRO POR ID
        public async Task<dynamic> ObterPorIdAsync(int id)
        {
            try
            {
                string connStr = ConfigurationManager.GetConnectionString();

                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    string sql = @"
                        SELECT 
                            u.IdUsuario, u.Codigo, u.Nome, u.Cpf, u.Cadastro, u.Email, 
                            u.Telefone, u.Celular, u.Senha, u.IdTabela, u.IdStatus, 
                            u.UsuarioInclusao, u.DataInclusao, u.UsuarioAlteracao, u.DataAlteracao
                        FROM Usuarios u
                        WHERE u.IdUsuario = @Id";

                    var usuario = await _db.QueryFirstOrDefaultAsync<UsuarioTeste>(sql, new { Id = id });
                    if (usuario != null) return usuario;
                }
            }
            catch
            {
                // Tratamento de exceção de conexão
            }

            var listagem = await ObterListagemAsync();
            return listagem.FirstOrDefault(x => x.IdUsuario == id);
        }

        // 3. GRAVAR / SALVAR REGISTRO (RETORNANDO RESULTADOOPERACAO COM MENSAGENS CUSTOMIZADAS)
        public async Task<ResultadoOperacao> SalvarAsync(object item)
        {
            if (item == null)
            {
                return ResultadoOperacao.Falha("Nenhum dado foi fornecido para a gravação.");
            }

            // A. DESSERIALIZAÇÃO / CONVERSÃO DO OBJETO
            UsuarioTeste usuario = null;

            if (item is UsuarioTeste u)
            {
                usuario = u;
            }
            else if (item is JsonElement jsonElem)
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                usuario = JsonSerializer.Deserialize<UsuarioTeste>(jsonElem.GetRawText(), options);
            }

            if (usuario == null)
            {
                return ResultadoOperacao.Falha("Os dados do usuário estão em um formato inválido.");
            }

            // B. VALIDAÇÕES ESPECÍFICAS DE REGRA DE NEGÓCIO
            if (string.IsNullOrWhiteSpace(usuario.Nome))
            {
                return ResultadoOperacao.Falha("O campo Nome é obrigatório.");
            }

            if (usuario.Nome.Trim().Length < 3)
            {
                return ResultadoOperacao.Falha("O nome do usuário deve conter pelo menos 3 caracteres.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Codigo))
            {
                return ResultadoOperacao.Falha("O código/login de acesso é obrigatório.");
            }

            if (usuario.IdStatus <= 0)
            {
                return ResultadoOperacao.Falha("Selecione um status válido para o usuário.");
            }

            // Sanitização do CPF (Remove pontos, traços e espaços)
            if (!string.IsNullOrEmpty(usuario.Cpf))
            {
                usuario.Cpf = Regex.Replace(usuario.Cpf, @"[^\d]", "");
            }

            // C. EXECUÇÃO DO SQL NO BANCO DE DADOS
            try
            {
                string connStr = ConfigurationManager.GetConnectionString();

                if (!string.IsNullOrEmpty(connStr) && _db != null)
                {
                    if (usuario.IdUsuario == 0)
                    {
                        // INCLUSÃO (INSERT)
                        string sqlInsert = @"
                            INSERT INTO Usuarios 
                                (Codigo, Nome, Cpf, Email, Telefone, Celular, Senha, IdTabela, IdStatus, UsuarioInclusao, DataInclusao)
                            VALUES 
                                (@Codigo, @Nome, @Cpf, @Email, @Telefone, @Celular, @Senha, @IdTabela, @IdStatus, 2, GETDATE());";

                        int linhasAfetadas = await _db.ExecuteAsync(sqlInsert, usuario);

                        return linhasAfetadas > 0
                            ? ResultadoOperacao.OK("Usuário cadastrado com sucesso!")
                            : ResultadoOperacao.Falha("Não foi possível cadastrar o usuário. Nenhuma linha foi afetada.");
                    }
                    else
                    {
                        // ALTERAÇÃO (UPDATE)
                        string sqlUpdate = @"
                            UPDATE Usuarios SET 
                                Codigo = @Codigo, 
                                Nome = @Nome, 
                                Cpf = @Cpf, 
                                Email = @Email, 
                                Telefone = @Telefone, 
                                Celular = @Celular, 
                                Senha = @Senha, 
                                IdTabela = @IdTabela, 
                                IdStatus = @IdStatus
                            WHERE IdUsuario = @IdUsuario;";

                        int linhasAfetadas = await _db.ExecuteAsync(sqlUpdate, usuario);

                        return linhasAfetadas > 0
                            ? ResultadoOperacao.OK("Usuário atualizado com sucesso!")
                            : ResultadoOperacao.Falha("Nenhum registro de usuário foi alterado.");
                    }
                }

                return ResultadoOperacao.Falha("Conexão com o banco de dados não configurada.");
            }
            catch (Exception ex)
            {
                // TRATAMENTO DE EXCEÇÕES CONHECIDAS DO SQL SERVER
                if (ex.Message.Contains("USUARIOS_STATUS_FK"))
                {
                    return ResultadoOperacao.Falha("O status selecionado não existe na tabela de status.");
                }

                if (ex.Message.Contains("FOREIGN KEY"))
                {
                    return ResultadoOperacao.Falha("Existe uma chave estrangeira vinculada a um registro inexistente.");
                }

                if (ex.Message.Contains("UQ_") || ex.Message.Contains("PRIMARY KEY") || ex.Message.Contains("duplicate key"))
                {
                    return ResultadoOperacao.Falha("Já existe um usuário cadastrado com este Código ou CPF.");
                }

                return ResultadoOperacao.Falha($"Erro ao salvar no banco de dados: {ex.Message}");
            }
        }
    }
}