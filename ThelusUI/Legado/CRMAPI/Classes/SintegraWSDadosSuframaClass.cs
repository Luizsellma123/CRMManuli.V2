using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class SintegraWSDadosSuframaClass
    {
        public string code { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string nome_empresarial { get; set; }
        public string cnpj { get; set; }
        public string inscricao_suframa { get; set; }
        public string endereco_eletronico { get; set; }
        public string telefone { get; set; }
        public string situacao_cadastral { get; set; }
        public string data_validade_cadastral { get; set; }
        public SintegraWSDadosSuframaNaturezaJuridicaClass natureza_juridica { get; set; }
        public SintegraWSDadosSuframaEnderecoClass endereco { get; set; }
        public SintegraWSDadosSuframaAtividadePrincipalClass atividade_principal { get; set; }
        public List<SintegraWSDadosSuframaAtividadeSecundariaClassClass> atividade_secundaria { get; set; }
        public List<SintegraWSDadosSuframaIncentivoClass> incentivos { get; set; }
        public string url_comprovante { get; set; }
    }
}