using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using VendasWeb;
using VendasWeb.classes;

namespace CRMAPI.Models
{
    public class FinalizaChamadoModel
    {
        public int IDChamado { get; set; }

        private ChamadoClass objChamado = new ChamadoClass();

        private ParametroGeral objParametroGeral = new ParametroGeral();

        public string FinalizarChamados()
        {
            DataTable chamadosFinalizados = objChamado.CarregaListaChamadosHomologados();

            //Numero de dias para finalizar os chamados
            int dias = objParametroGeral.RetornaValorNumericoParametro("DIASFINALIZARCHAMADOHOMOLOGANDO");

            if (chamadosFinalizados.Rows.Count > 0)
            {
                string erro = "";

                foreach (DataRow row in chamadosFinalizados.Rows)
                {
                    if (Convert.ToDateTime(row["DataHomologacao"]).AddDays(dias) <= DateTime.Today)
                    {
                        objChamado.NumeroChamado = Convert.ToInt32(row["IDChamado"]);

                        erro = objChamado.GravaChamadoFinalizado();

                        if (erro != "") return erro;
                    }
                }
            }

            return "";
        }
    }
}