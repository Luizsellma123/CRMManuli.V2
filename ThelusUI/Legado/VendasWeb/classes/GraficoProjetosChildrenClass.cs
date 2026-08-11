using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendasWeb.classes
{
    public class GraficoProjetosChildrenClass
    {
        public string id { get; set; }
        public string name { get; set; }
        public string actualStart { get; set; }
        public string actualEnd { get; set; }
        public string connectTo { get; set; }
        public string connectorType { get; set; }
        public string progressValue { get; set; }
    }
}