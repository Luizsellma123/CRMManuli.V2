using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ClienteEnderecoClass
    {
        public string CodigoClienteSAP { get; set; }
        public string DescricaoEndereco { get; set; }
        public string TipoLogradouro { get; set; }
        public string Rua { get; set; }
        public string NumeroRua { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string PaisSAP { get; set; }
        public string EstadoSAP { get; set; }
        public string MunicipioSAP { get; set; }
    }
}