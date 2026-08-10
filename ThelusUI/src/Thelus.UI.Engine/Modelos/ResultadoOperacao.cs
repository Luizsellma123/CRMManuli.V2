using System.Collections.Generic;

namespace Thelus.UI.Engine.Modelos
{
    public class ResultadoOperacao
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public List<string> Erros { get; set; } = new();

        public static ResultadoOperacao OK(string mensagem = "Operação realizada com sucesso!")
        {
            return new ResultadoOperacao { Sucesso = true, Mensagem = mensagem };
        }

        public static ResultadoOperacao Falha(string mensagem, List<string> erros = null)
        {
            return new ResultadoOperacao
            {
                Sucesso = false,
                Mensagem = mensagem,
                Erros = erros ?? new List<string>()
            };
        }
    }
}