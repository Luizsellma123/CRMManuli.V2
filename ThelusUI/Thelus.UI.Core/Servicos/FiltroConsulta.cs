using System.Collections.Generic;

namespace Thelus.Core.Servicos
{
    public class FiltroConsulta
    {
        public string EntityName { get; set; }
        public string TermoBusca { get; set; }
        public Dictionary<string, object> Parametros { get; set; } = new Dictionary<string, object>();
        public int Pagina { get; set; } = 1;
        public int TamanhoPagina { get; set; } = 50;
    }
}