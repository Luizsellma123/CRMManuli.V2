using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ComunicacaoServiceLayerRetornoErrorSAPClass
    {
        public int code { get; set; }
        public ComunicacaoServiceLayerRetornoErrorMessageSAPClass message { get; set; }
    }
}