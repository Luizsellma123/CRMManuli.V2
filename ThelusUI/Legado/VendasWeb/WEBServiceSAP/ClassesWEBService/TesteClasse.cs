using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace VendasWeb.WEBServiceSAP.ClassesWEBService
{
    [XmlRoot("serviceResponse")]
    public class TesteClasse
    {
        [System.Xml.Serialization.XmlElement("responseBody")]
        public Teste2Classe[] teste { get; set; }
    }
}