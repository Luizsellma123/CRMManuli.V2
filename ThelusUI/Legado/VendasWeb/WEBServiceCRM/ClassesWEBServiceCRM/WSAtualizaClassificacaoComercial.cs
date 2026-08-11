using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    [DataContract]
    public class WSAtualizaClassificacaoComercial
    {
        [DataMember]
        public string CodigoClienteSAP { get; set; }

        [DataMember]
        public string ClassificacaoComercialSAP { get; set; }      
    }
}