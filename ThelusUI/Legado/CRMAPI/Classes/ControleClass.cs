using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ControleClass
    {
        public string ReiniciaPoll()
        {
            string erro = "";

            try
            {
                HttpRuntime.UnloadAppDomain();
            }
            catch
            {
                erro = "Não foi possivel encerrar aplicação.";
            }

            return erro;
        }
    }
}