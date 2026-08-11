using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CRMAPI.Models
{
    public class ClassificacaoComercialModel
    {
        public string CodigoClienteSAP { get; set; }
        public string ClassificacaoComercialSAP { get; set; }

        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        public string SalvaClassificacaoComercial()
        {
            string erro = "";

            OBJComunicacaoSAP.CodigoClienteSAP = this.CodigoClienteSAP;
            OBJComunicacaoSAP.ClassificacaoComercialSAP = this.ClassificacaoComercialSAP;

            erro = OBJComunicacaoSAP.AtualizarClassificacaoComercialCliente();

            return erro;
        }

    }
}