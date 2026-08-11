using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.classes
{
    public class InfraestruturaEmailClass
    {
        public string EmailRemetente { get; set; }

        public string EmailRemetenteSenha { get; set; }

        public string EmailHost { get; set; }

        public string EmailPort { get; set; }

        public string EmailDestinatario { get; set; }

        public string IntervaloEmailminutos { get; set; }

        public string LimiteUsoCPUPorcentagem { get; set; }

        public string LimiteUsoRAMPorcentagem { get; set; }

        public string LimiteUsoDiscoPorcentagem { get; set; }

        public string Alertar { get; set; }

        public string UltimoAlerta { get; set; }
    }
}