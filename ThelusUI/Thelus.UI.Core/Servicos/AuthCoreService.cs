using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;

namespace Thelus.Core.Servicos
{
    public class AuthCoreService : IAuthCoreService
    {
        // Inject do seu acesso ao banco (DatabaseAccess, DbContext ou Dapper)
        // private readonly DatabaseAccess _db;

        public async Task<LoginResponseDto> AutenticarAsync(LoginRequestDto request)
        {
            // 1. Validação defensiva dos 3 parâmetros obrigatórios
            if (request == null || request.EmpresaId <= 0 || string.IsNullOrWhiteSpace(request.Usuario) || string.IsNullOrWhiteSpace(request.Senha))
            {
                return new LoginResponseDto
                {
                    Sucesso = false,
                    Mensagem = "Empresa, usuário e senha são obrigatórios."
                };
            }

            try
            {
                // =========================================================================
                // 2. CONSULTA NO BANCO CRUZANDO OS 3 DADOS
                // Exemplo da instrução SQL que deve ser executada no banco:
                /*
                    SELECT u.Id, u.Nome, u.IdPerfil
                    FROM Usuarios u
                    WHERE u.IdEmpresa = @EmpresaId 
                      AND (u.Login = @Usuario OR u.Email = @Usuario)
                      AND u.Senha = @Senha
                      AND u.Ativo = 1
                */
                // =========================================================================

                // Simulação para testes mantendo o escopo da empresa
                bool usuarioValido = request.EmpresaId > 0 &&
                                     !string.IsNullOrWhiteSpace(request.Usuario) &&
                                     request.Senha == "123456";

                if (!usuarioValido)
                {
                    return new LoginResponseDto
                    {
                        Sucesso = false,
                        Mensagem = "Empresa, usuário ou senha inválidos."
                    };
                }

                // =========================================================================
                // 3. BUSCA OS MENUS DA EMPRESA ESPECÍFICA
                // Garante que o menu/permissão retornado seja exclusivo daquela Empresa
                /*
                    SELECT DISTINCT p.IdMenu
                    FROM UsuarioPerfil up
                    INNER JOIN PerfilPermissao p ON up.IdPerfil = p.IdPerfil
                    WHERE up.IdUsuario = @IdUsuario 
                      AND up.IdEmpresa = @EmpresaId
                */
                // =========================================================================
                var idsMenuPermitidos = new List<int>
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20,
                    21, 22, 23, 24, 25, 26, 28, 29, 31, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44
                };

                return new LoginResponseDto
                {
                    Sucesso = true,
                    NomeUsuario = request.Usuario,
                    Token = Guid.NewGuid().ToString(),
                    IdsMenuPermitidos = idsMenuPermitidos,
                    Mensagem = "Autenticado com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseDto
                {
                    Sucesso = false,
                    Mensagem = $"Erro ao validar autenticação: {ex.Message}"
                };
            }
        }
    }
}