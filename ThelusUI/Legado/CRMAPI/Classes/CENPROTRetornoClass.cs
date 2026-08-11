using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CRMAPI.Classes
{
    public class CENPROTRetornoClass
    {
        public MetaDados metaDados { get; set; }

        public Retorno retorno { get; set; }

        public class MetaDados
        {
            public string consultaNome { get; set; }

            public string consultaUid { get; set; }

            public string chave { get; set; }

            public string usuario { get; set; }

            public string mensagem { get; set; }

            public string ip { get; set; }

            public int resultadoId { get; set; }

            public string resultado { get; set; }

            public string apiVersao { get; set; }

            public bool enviarCallback { get; set; }

            public bool gerarComprovante { get; set; }

            public string urlComprovante { get; set; }

            public bool assincrono { get; set; }

            public string data { get; set; }

            public int tempoExecucaoMs { get; set; }
        }

        public class Retorno
        {
            public string documentoConsultado { get; set; }

            public bool constamProtestos { get; set; }

            public int numeroTotalProtestos { get; set; }

            public string valorTotalProtestos { get; set; }

            public string observacoes { get; set; }

            public List<ProtestoUf> protestos { get; set; }
        }

        public class ProtestoUf
        {
            public string estado { get; set; }

            public int numeroTotalProtestosUF { get; set; }

            public string valorTotalProtestosEstado { get; set; }

            public List<Cartorio> cartorios { get; set; }
        }

        public class Cartorio
        {
            public string codigoCidade { get; set; }

            public string cidade { get; set; }

            public int numeroProtestos { get; set; }

            public string valorTotalProtestosCartorio { get; set; }

            public List<TituloProtestado> titulos { get; set; }
        }

        public class TituloProtestado
        {
            public string dataProtesto { get; set; }

            public string valorProtestado { get; set; }

            public string documento { get; set; }
        }
    }
}

//Antigos
/*

public int code { get; set; }

//principal mensagem de retorno
public string code_message { get; set; }

public Header header { get; set; }

public class Header
{
    public string api_version { get; set; }
    public string api_version_full { get; set; }
    public string product { get; set; }
    public string service { get; set; }

    public Parameters parameters { get; set; }

    public class Parameters
    {
        public string cnpj { get; set; }
    }

    public string client_name { get; set; }
    public string token_name { get; set; }
    public bool billable { get; set; }
    public string price { get; set; }
    public string requested_at { get; set; }
    public int elapsed_time_in_milliseconds { get; set; }
    public string remote_ip { get; set; }
    public string signature { get; set; }
}

public int data_count { get; set; }

public List<Data> data { get; set; }

public class Data
{
    public Cartorios cartorios { get; set; }

    public class Cartorios
    {
        public List<Cartorio> AC { get; set; }
        public List<Cartorio> AL { get; set; }
        public List<Cartorio> AP { get; set; }
        public List<Cartorio> AM { get; set; }
        public List<Cartorio> BA { get; set; }
        public List<Cartorio> CE { get; set; }
        public List<Cartorio> DF { get; set; }
        public List<Cartorio> ES { get; set; }
        public List<Cartorio> GO { get; set; }
        public List<Cartorio> MA { get; set; }
        public List<Cartorio> MT { get; set; }
        public List<Cartorio> MS { get; set; }
        public List<Cartorio> MG { get; set; }
        public List<Cartorio> PA { get; set; }
        public List<Cartorio> PB { get; set; }
        public List<Cartorio> PR { get; set; }
        public List<Cartorio> PE { get; set; }
        public List<Cartorio> PI { get; set; }
        public List<Cartorio> RJ { get; set; }
        public List<Cartorio> RN { get; set; }
        public List<Cartorio> RS { get; set; }
        public List<Cartorio> RO { get; set; }
        public List<Cartorio> RR { get; set; }
        public List<Cartorio> SC { get; set; }
        public List<Cartorio> SP { get; set; }
        public List<Cartorio> SE { get; set; }
        public List<Cartorio> TO { get; set; }

        public class Cartorio
        {
            public string codigo { get; set; }
            public string obter_detalhes { get; set; }
            public string nome { get; set; }
            public string telefone { get; set; }
            public string endereco { get; set; }
            public string cidade_codigo { get; set; }
            public string cidade_codigo_ibge { get; set; }
            public string municipio { get; set; }
            public string cidade { get; set; }
            public string bairro { get; set; }
            public string atualizacao_data { get; set; }
            public string quantidade { get; set; }
            public string periodo_pesquisa { get; set; }

            public List<Protesto> protestos { get; set; }

            public class Protesto
            {
                public string cpf_cnpj { get; set; }
                public string data { get; set; }
                public string data_protesto { get; set; }
                public string data_protesto_string { get; set; }
                public string data_vencimento { get; set; }
                public string data_vencimento_string { get; set; }
                public decimal valor { get; set; }
                public string valor_string { get; set; }
                public string chave { get; set; }
                public string nome_apresentante { get; set; }
                public string nome_cedente { get; set; }
                public string tem_anuencia { get; set; }
            }
        }
    }

    public string consulta_data { get; set; }
    public string consulta_datahora { get; set; }
    public string documento_pesquisado { get; set; }
    public int quantidade_titulos { get; set; }
    public string site_receipt { get; set; }
}

public List<Error> errors { get; set; }

public class Error
{

}

public List<string> site_receipts { get; set; }

*/