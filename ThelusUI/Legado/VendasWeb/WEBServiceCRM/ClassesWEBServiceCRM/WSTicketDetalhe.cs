using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.GerencialVendas;
using VendasWeb.classes;
using System.Data;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSTicketDetalhe
    {
        #region Campos Principais

        public int IDEmpresa { get; set; }

        public int IDTicket { get; set; }

        public string Empresa { get; set; }

        public string Cliente { get; set; }

        public string Solicitante { get; set; }

        public string Responsavel { get; set; }

        public string Tratativa { get; set; }

        public string Situacao { get; set; }

        public string Abertura { get; set; }

        public string Fechamento { get; set; }

        public string Prioridade { get; set; }

        public string Solucao { get; set; }

        public string Ocorrencia { get; set; }

        public string Vendedor { get; set; }

        public string Motivo { get; set; }

        public string Descricao { get; set; }

        #endregion

        SACClass ObjSAC = new SACClass();

        DataTable ObjDataTable = new DataTable();

        public void RetornaListaTicketsDetalhe()
        {
            ObjSAC.Tela = "WSTicketDetalhe";
            ObjSAC.IDEmpresa = Convert.ToInt32(this.IDEmpresa);
            ObjSAC.IDTicket = Convert.ToInt32(this.IDTicket);
            ObjSAC.IDSituacao = 0;
            ObjSAC.Cliente = "";
            ObjDataTable = ObjSAC.RetornaListaTickets();

            if (ObjDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ObjDataTable.Rows)
                {
                    this.Empresa = row["Empresa"].ToString();
                    this.Cliente = row["Cliente"].ToString();
                    this.Solicitante = row["Solicitante"].ToString();
                    this.Responsavel = row["Responsavel"].ToString();
                    this.Tratativa = row["Tratativa"].ToString();
                    this.Situacao = row["Situacao"].ToString();
                    this.Abertura = row["Abertura"].ToString();
                    this.Fechamento = row["Fechamento"].ToString();
                    this.Prioridade = row["Prioridade"].ToString();
                    this.Solucao = row["Solucao"].ToString();
                    this.Ocorrencia = row["Ocorrencia"].ToString();
                    this.Vendedor = row["Vendedor"].ToString();
                    this.Motivo = row["Motivo"].ToString();
                    this.Descricao = row["Descricao"].ToString();
                }
            }
        }

    }
}