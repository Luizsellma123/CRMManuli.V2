using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class ClienteEnderecoModel
    {
        public string id_endereco { get; set; }
        public string tipo_endereco { get; set; }
        public string rua { get; set; }
        public string numero { get; set; }
        public string complemento { get; set; }
        public string cep { get; set; }
        public string bairro { get; set; }
        public string cidade { get; set; }
        public string estado { get; set; }
        public string municipio { get; set; }
        public string pais { get; set; }
        public string tipo_logradouro { get; set; }
    }
}