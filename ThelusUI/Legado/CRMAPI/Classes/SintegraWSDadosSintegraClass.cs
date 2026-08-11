using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class SintegraWSDadosSintegraClass
    {
        public string code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string nome_empresarial { get; set; }
        public string cnpj { get; set; }
        public string inscricao_estadual { get; set; }
        public string tipo_inscricao { get; set; }
        public string data_situacao_cadastral { get; set; }
        public string situacao_cnpj { get; set; }
        public string situacao_ie { get; set; }
        public string nome_fantasia { get; set; }
        public string data_inicio_atividade { get; set; }
        public string regime_tributacao { get; set; }
        public string informacao_ie_como_destinatario { get; set; }
        public string porte_empresa { get; set; }
        public SintegraWSDadosSintegraCnaePrincipalClass cnae_principal { get; set; }
        public string data_fim_atividade { get; set; }
        public string uf { get; set; }
        public string municipio { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string cep { get; set; }
        public string numero { get; set; }
        public string bairro { get; set; }
        public SintegraWSDadosSintegraClassIbgeClass ibge { get; set; }
    }
}