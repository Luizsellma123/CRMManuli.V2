using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ClienteAnexoClass
    {
        public string CodigoClienteSAP { get; set; }
        public int IDAnexoSAP { get; set; }
        public int CodigoSAP { get; set; }
        public string CaminhoDestino { get; set; }
        public string NomeArquivo { get; set; }
        public string ExtensaoArquivo { get; set; }
        public string DataAnexo { get; set; }
        public string TextoLivre { get; set; }
    }
}