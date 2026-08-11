using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VendasWeb.classes;
using VendasWeb.GerencialVendas;

namespace VendasWeb.Controladoria.Fretes
{
    public partial class CenarioEstadosWebForm : System.Web.UI.Page
    {
        FretesClass CenarioFrete = new FretesClass();
        UtilClass ObjUtilClass = new UtilClass();
        SessionClass OBJSessao = new SessionClass();

        protected void Page_Load(object sender, EventArgs e)
        {
            //Valida Acesso
            OBJSessao.ValidaAcesso();

            if (!IsPostBack)
            {
                PainelFiltrosLiteral.Text = "<div class=\"collapse in\" id=\"filtros\" aria-expanded=\"true\">";
            }
        }

        protected void RetornarButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("../FreteWebForm.aspx?indmnu=3");
        }

        protected void PadraoButton_Click(object sender, EventArgs e)
        {
            //Buscando planilha padrão
            string DocEntPathArq = Server.MapPath("~/Controladoria/Cenarios/Padrao.xlsx");

            //Lendo e Criando arquivo para Download
            System.IO.FileStream fs = new System.IO.FileStream(DocEntPathArq, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] ar = new byte[(int)fs.Length];
            fs.Read(ar, 0, (int)fs.Length);
            fs.Close();

            //Pegando nome do Arquivo
            string fileName = Path.GetFileName(fs.Name);

            //Enviando requisicao de Download
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.ContentType = "application/octectstream";
            Response.BinaryWrite(ar);
            Response.End();
        }
    }
}