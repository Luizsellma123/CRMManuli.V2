using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.classes
{
    public class senha
    {
        public string ValidaIntegridadeSenhaUsuario(string senha)
        {
            if (senha.Length < 8)
            {
                return "A senha deve ter no mínimo 8 caracteres.";
            }

            if (ContemSequencia(senha))
            {
                return "A senha não deve conter sequências de caracteres ou números.";
            }

            return "";
        }

        /*
        public string ValidaIntegridadeSenhaUsuario(string senha)
        {
            // Verificação básica
            if (string.IsNullOrWhiteSpace(senha))
                return "A senha não pode estar vazia. (Exigência padrão das normas de segurança da informação)";

            // Requisitos mínimos conforme boas práticas (ISO 27001 / NIST SP 800-63B)
            var requisitos = new List<string>();

            if (senha.Length < 8)
                requisitos.Add("ter no mínimo 8 caracteres");

            if (!senha.Any(char.IsUpper))
                requisitos.Add("conter pelo menos uma letra MAIÚSCULA");

            if (!senha.Any(char.IsLower))
                requisitos.Add("conter pelo menos uma letra minúscula");

            if (!senha.Any(char.IsDigit))
                requisitos.Add("conter pelo menos um número");

            if (!senha.Any(ch => "!@#$%^&*()-_=+[]{}|;:'\",.<>?/\\`~".Contains(ch)))
                requisitos.Add("conter pelo menos um caractere especial (ex: !, @, #, $...)");

            if (ContemSequencia(senha))
                requisitos.Add("não conter sequências ou repetições óbvias (tipo 123, abc ou aaa)");
            
            if (requisitos.Any())
            {
                return
                    "🔒 Conforme as diretrizes de segurança da informação estabelecidas pelas normas ISO 27001 e NIST SP 800-63B, " +
                    "a senha informada não atende aos requisitos mínimos. <br> Ela deve:<br>" +
                    "• " + string.Join("<br>• ", requisitos) + "<br>" +
                    "Essas regras visam garantir a integridade e a proteção dos dados de acesso.";
            }

            return ""; // senha válida
        }
       */

        private bool ContemSequencia(string senha)
        {
            // Verifica se há caracteres sequenciais repetidos
            for (int i = 0; i < senha.Length - 1; i++)
            {
                if (senha[i] == senha[i + 1])
                {
                    return true;
                }
            }

            // Verifica se há sequências numéricas ou alfabéticas
            for (int i = 0; i < senha.Length - 2; i++)
            {
                if ((senha[i] + 1 == senha[i + 1] && senha[i + 1] + 1 == senha[i + 2]) ||
                    (senha[i] - 1 == senha[i + 1] && senha[i + 1] - 1 == senha[i + 2]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}