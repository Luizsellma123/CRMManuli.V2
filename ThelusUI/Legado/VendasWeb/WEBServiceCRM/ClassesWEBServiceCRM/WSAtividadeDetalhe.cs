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
    public class WSAtividadeDetalhe
    {
        #region Campos Principais

        public int IDEmpresa { get; set; }

        public int IDTicket { get; set; }

        public int IDAtividade { get; set; }

        #endregion

        #region Campos

        public string Empresa { get; set; }

        public string Cliente { get; set; }

        public string Solicitante { get; set; }

        //public string Ticket { get; set; }

        public string Situacao { get; set; }

        public string Assunto { get; set; }

        public string Classificacao { get; set; }

        public string Data { get; set; }

        public string Prioridade { get; set; }

        public string Descricao { get; set; }

        public string Setor { get; set; }

        public string Responsavel { get; set; }

        public string AssuntoAtividade { get; set; }

        public string DescricaoAtividade { get; set; }

        #endregion

        SACClass ObjSAC = new SACClass();

        DataTable ObjDataTable = new DataTable();

        public void RetornaListaAtividadesDetalhe()
        {
            ObjSAC.Tela = "WSAtividadeDetalhe";
            ObjSAC.IDEmpresa = Convert.ToInt32(this.IDEmpresa);
            ObjSAC.IDSetor = 0;
            ObjSAC.IDTicket = Convert.ToInt32(this.IDTicket);
            ObjSAC.Cliente = "";
            ObjSAC.Solicitante = "";
            ObjSAC.IDAtividade = Convert.ToInt32(this.IDAtividade);
            ObjSAC.IDSituacao = 0;
            ObjSAC.DataInicio = "";
            ObjSAC.DataFim = "";
            ObjSAC.Administrador = "0";
            ObjSAC.IDUsuario = 0;

            ObjDataTable = ObjSAC.RetornaListaAtividades();

            if (ObjDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ObjDataTable.Rows)
                {
                    this.Empresa = Convert.ToString(row["Empresa"]);
                    this.Cliente = Convert.ToString(row["Cliente"]);
                    this.Solicitante = Convert.ToString(row["Solicitante"]);
                    //this.Ticket = Convert.ToString(this.IDTicket);
                    this.Situacao = Convert.ToString(row["Situacao"]);
                    this.Assunto = Convert.ToString(row["Assunto"]);
                    this.Descricao = Convert.ToString(row["Descricao"]);
                    this.Classificacao = Convert.ToString(row["Classificacao"]);
                    this.Data = Convert.ToDateTime(row["Data"]).ToString("dd/MM/yyyy");
                    if (this.Data == "01-01-1900") this.Data = "";
                    this.Prioridade = Convert.ToString(row["Prioridade"]);
                    //this.Atividade = Convert.ToString(this.IDAtividade);
                    this.Setor = Convert.ToString(row["Setor"]);
                    this.Responsavel = Convert.ToString(row["Responsavel"]);
                    this.AssuntoAtividade = Convert.ToString(row["AssuntoAtividade"]);
                    this.DescricaoAtividade = Convert.ToString(row["DescricaoAtividade"]);
                }
            }
        }


    }
}