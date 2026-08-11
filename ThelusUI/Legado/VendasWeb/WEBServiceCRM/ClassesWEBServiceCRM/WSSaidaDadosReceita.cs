using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSSaidaDadosReceita
    {
        public string CadastroContribuiente { get; set; }
        public string IsentoIE { get; set; }
        public string PossuiSimplesNacional { get; set; }
        public string PossuiSuframa { get; set; }

        public class SintegraWSDadosSimplesNacional_Class
        {
            public string agendamentos { get; set; }
            public string cnpj { get; set; }
            public string cnpj_matriz { get; set; }
            public string code { get; set; }
            public string eventos_futuros_simples_nacional { get; set; }
            public string eventos_futuros_simples_simei { get; set; }
            public string message { get; set; }
            public string nome_empresarial { get; set; }
            public string situacao_simei { get; set; }
            public string situacao_simei_anterior { get; set; }
            public string situacao_simples_nacional { get; set; }
            public string situacao_simples_nacional_anterior { get; set; }
            public string status { get; set; }
        }

        public SintegraWSDadosSimplesNacional_Class SintegraWSDadosSimplesNacional { get; set; }

        public class SintegraWSDadosSintegra_Class
        {
            public string bairro { get; set; }
            public string cep { get; set; }

            public class cnae_principal_Class
            {
                public string code { get; set; }
                public string text { get; set; }
            }

            public cnae_principal_Class cnae_principal { get; set; }

            public string cnpj { get; set; }
            public string code { get; set; }
            public string complemento { get; set; }
            public string data_fim_atividade { get; set; }
            public string data_inicio_atividade { get; set; }
            public string data_situacao_cadastral { get; set; }

            public class ibge_Class
            {
                public string codigo_municipio { get; set; }
                public string codigo_uf { get; set; }
            }

            public ibge_Class ibge { get; set; }

            public string informacao_ie_como_destinatario { get; set; }
            public string inscricao_estadual { get; set; }
            public string logradouro { get; set; }
            public string message { get; set; }
            public string municipio { get; set; }
            public string nome_empresarial { get; set; }
            public string nome_fantasia { get; set; }
            public string numero { get; set; }
            public string porte_empresa { get; set; }
            public string regime_tributacao { get; set; }
            public string situacao_cnpj { get; set; }
            public string situacao_ie { get; set; }
            public string status { get; set; }
            public string tipo_inscricao { get; set; }
            public string uf { get; set; }
        }

        public SintegraWSDadosSintegra_Class SintegraWSDadosSintegra { get; set; }

        public class SintegraWSDadosSuframa_Class
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

            public class natureza_juridica_Class
            {
                public string codigo { get; set; }
                public string descricao { get; set; }
            }

            public natureza_juridica_Class natureza_juridica { get; set; }

            public class endereco_Class
            {
                public string logradouro { get; set; }
                public string numero { get; set; }
                public string complemento { get; set; }
                public string bairro { get; set; }
                public string cep { get; set; }
                public string municipio { get; set; }
                public string uf { get; set; }
            }

            public endereco_Class endereco { get; set; }

            public class atividade_principal_Class
            {
                public string codigo { get; set; }
                public string descricao { get; set; }
                public string atividade_exercida { get; set; }
            }

            public atividade_principal_Class atividade_principal { get; set; }

            /*
            public class atividade_secundaria_Class
            {
                public string codigo { get; set; }
                public string descricao { get; set; }
                public string atividade_exercida { get; set; }
            }
            */

            public List<atividade_principal_Class> atividade_secundaria { get; set; }

            public class incentivos_Class
            {
                public string tributo { get; set; }
                public string beneficio { get; set; }
                public string finalidade { get; set; }
                public string base_legal { get; set; }
            }

            public List<incentivos_Class> incentivos { get; set; }

            public class file_return_Class
            {
                public string ext_file { get; set; }
                public string url_file { get; set; }
            }

            public file_return_Class file_return { get; set; }

            public string version { get; set; }
        }

        public SintegraWSDadosSuframa_Class SintegraWSDadosSuframa { get; set; }

        public string Suframa { get; set; }
        public string abertura { get; set; }

        public class atividade_principal_Class
        {
            public string code { get; set; }
            public string text { get; set; }
        }

        public List<atividade_principal_Class> atividade_principal { get; set; }

        public class atividades_secundarias_Class
        {
            public string code { get; set; }
            public string text { get; set; }
        }

        public List<atividades_secundarias_Class> atividades_secundarias { get; set; }

        public string bairro { get; set; }

        public class billing_Class
        {
            public string database { get; set; }
            public string free { get; set; }
        }

        public billing_Class billing { get; set; }

        public string capital_social { get; set; }
        public string cep { get; set; }
        public string cnpj { get; set; }
        public string complemento { get; set; }
        public string data_situacao { get; set; }
        public string data_situacao_especial { get; set; }
        public string efr { get; set; }
        public string email { get; set; }
        public string extra { get; set; }
        public string fantasia { get; set; }
        public string logradouro { get; set; }
        public string message { get; set; }
        public string motivo_situacao { get; set; }
        public string municipio { get; set; }
        public string natureza_juridica { get; set; }
        public string nome { get; set; }
        public string numero { get; set; }
        public string porte { get; set; }

        public class qsa_Class
        {
            public string nome { get; set; }
            public string qual { get; set; }
        }

        public qsa_Class qsa { get; set; }

        public string situacao { get; set; }
        public string situacao_especial { get; set; }
        public string status { get; set; }
        public string telefone { get; set; }
        public string tipo { get; set; }
        public string uf { get; set; }
        public string ultima_atualizacao { get; set; }

    }
}