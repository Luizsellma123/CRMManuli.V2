using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class DadosReceitaRetornoClass
    {
        public string status { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public List<string> constraints { get; set; }
    }
}