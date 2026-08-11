using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class SintegraWSDadosSimplesNacionalClass
    {
        public string code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string cnpj { get; set; }
        public string cnpj_matriz { get; set; }
        public string nome_empresarial { get; set; }
        public string situacao_simples_nacional { get; set; }
        public string situacao_simei { get; set; }
        public string situacao_simples_nacional_anterior { get; set; }
        public string situacao_simei_anterior { get; set; }
        public string agendamentos { get; set; }
        public string eventos_futuros_simples_nacional { get; set; }
        public string eventos_futuros_simples_simei { get; set; }
    }
}