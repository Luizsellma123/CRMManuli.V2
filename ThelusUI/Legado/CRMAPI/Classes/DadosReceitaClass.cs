using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class DadosReceitaClass
    {
        public List<DadosReceitaAtividadePrincipalClass> atividade_principal { get; set; }
        public string data_situacao { get; set; }
        public string fantasia { get; set; }
        public string complemento { get; set; }
        public string tipo { get; set; }
        public string nome { get; set; }
        public string telefone { get; set; }
        public List<DadosReceitaAtividadesSecundariaClass> atividades_secundarias { get; set; }
        public List<DadosReceitaResponsaveisClass> qsa { get; set; }
        public string situacao { get; set; }
        public string bairro { get; set; }
        public string logradouro { get; set; }
        public string numero { get; set; }
        public string cep { get; set; }
        public string municipio { get; set; }
        public string porte { get; set; }
        public string abertura { get; set; }
        public string natureza_juridica { get; set; }
        public string uf { get; set; }
        public string cnpj { get; set; }
        public string ultima_atualizacao { get; set; }
        public string status { get; set; }
        public string email { get; set; }
        public string efr { get; set; }
        public string motivo_situacao { get; set; }
        public string situacao_especial { get; set; }
        public string data_situacao_especial { get; set; }
        public string capital_social { get; set; }
        public DadosReceitaExtraClass extra { get; set; }
        public DadosReceitaCobrancaClass billing { get; set; }
        public DadosReceitaCadastroContribuinteClass CadastroContribuiente { get; set; }
        public SintegraWSDadosSintegraClass SintegraWSDadosSintegra { get; set; }
        public SintegraWSDadosSuframaClass SintegraWSDadosSuframa { get; set; }
        public SintegraWSDadosSimplesNacionalClass SintegraWSDadosSimplesNacional { get; set; }
        public string PossuiSuframa { get; set; }
        public string PossuiSimplesNacional { get; set; }
        public string PossuiSimplesNacionalMEI { get; set; }
        public string IsentoIE { get; set; }
        public string message { get; set; }

        public DadosReceitaClass()
        {
            IsentoIE = "Sim";
            PossuiSuframa = "Não";
            PossuiSimplesNacional = "Não";
            PossuiSimplesNacionalMEI = "Não";
        }
    }
}