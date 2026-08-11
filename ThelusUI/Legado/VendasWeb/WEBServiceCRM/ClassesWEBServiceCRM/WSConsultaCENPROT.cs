using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Web;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    [DataContract]
    public class WSConsultaCENPROT
    {
        [DataMember]
        public int IDAnalise { get; set; }

        [DataMember]
        public int IDCliente { get; set; }
    }
}