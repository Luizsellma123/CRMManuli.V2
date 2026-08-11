using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using VendasWeb;
using VendasWeb.classes;

namespace CRMAPI.Models
{
    public class AvisaChamadoAprovacaoModel
    {
        public int IDChamado { get; set; }

        private ChamadoClass objChamado = new ChamadoClass();

        public string AvisaAprovacaoChamados()
        {
            DataTable keyUsers = objChamado.CarregaListaChamadosKeyUsers();

            if (keyUsers.Rows.Count > 0)
            {
                foreach (DataRow keyUser in keyUsers.Rows)
                {
                    if (Convert.ToInt32(keyUser["chamadosParaAprovar"]) > 0)
                    {
                        objChamado.Assunto = "Aprovação de chamados";

                        objChamado.descricao = "Há " + keyUser["chamadosParaAprovar"] + " chamado(s) que precisam da sua aprovação";

                        objChamado.CodigoUsuario = "CRM API";

                        string erro = objChamado.EnviaEmailKeyUser(keyUser["Email"].ToString());

                        if (erro != "") return erro;
                    }
                }
            }

            return "";
        }
    }
}