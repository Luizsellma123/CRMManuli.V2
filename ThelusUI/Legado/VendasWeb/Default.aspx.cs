using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Web.Services;
using System.Net.Http;
using System.ServiceModel;
using VendasWeb.WEBServiceSAP.ClassesWEBService;
using System.Web.Script.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using VendasWeb.WEBServiceSAP;

namespace VendasWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        funcoes OBJfuncoes = new funcoes();
        

        protected void Page_Load(object sender, EventArgs e)
        {
            //DataTable outputTable = new DataTable();
            //Redireciona para tela de login
            Response.Redirect("login.aspx?indmnu=0"); 

            //string aux = "select * from ONCM";
            //outputTable = OBJfuncoes.Executa_DataTable(aux,"buscar.");


            //ServicoComunicacaoSAPLocal.ComunicacaoSAPSoapClient Teste = new ServicoComunicacaoSAPLocal.ComunicacaoSAPSoapClient();
            //ComunicacaoSAP Teste = new ComunicacaoSAP();
            //var strPaises = Teste.Atualiza_Clientes();

            //string teste = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><serviceResponse serviceName=\"MobileLoginSP.login\" status=\"1\" pendingPrinting=\"false\" transactionId=\"3EDC206723CEB66D4536B827F9878937\" errorCode=\"-1\" errorLevel=\"-1\">    <responseBody>        <jsessionid>Wdq6zpV5kvNExuVzhfPtl7dXTkaiZM7G_dX82eIM</jsessionid>        <idusu>NQ==</idusu>        <callID>F8D29FD59B4996E7A56FFCB5DD70AD2D</callID>    </responseBody></serviceResponse>";

            //var xmlContent = new StringReader(teste);
            //XmlSerializer xml = new XmlSerializer(typeof(TesteClasse));
            //var pessoas = (TesteClasse)xml.Deserialize(xmlContent);

        }

    }
}