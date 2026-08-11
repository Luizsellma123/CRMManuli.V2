<%@ WebHandler Language="C#" Class="JsonMapsRota" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.SessionState;
using VendasWeb.GerencialVendas;
using System.Data;

public class JsonMapsRota : IHttpHandler, IRequiresSessionState
{
    /*****************************************************************************************************************************
        Json Utilizado no Maps para Rotas       
     ******************************************************************************************************************************/

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        MapsClass ObjMapsClass = new MapsClass();
        MapsClass ObjPontosNoCaminho = new MapsClass();
        List<MapsClass> tasksList = new List<MapsClass>();
        List<clsEntidades> ListObjEntidadesRotas = new List<clsEntidades>();
        
        DataTable outputTable = new DataTable();



        if (HttpContext.Current.Session["EntCodMaps"] != null)
        {

            ObjMapsClass.UsuCod = HttpContext.Current.Session["usuario"].ToString();


            if (HttpContext.Current.Session["ListObjEntidadesRotas"] != null)
            {
                ListObjEntidadesRotas = (List<clsEntidades>)HttpContext.Current.Session["ListObjEntidadesRotas"];

                for(int E = 0;E< ListObjEntidadesRotas.Count;E++)
                {
                    ObjMapsClass.EntCod += ListObjEntidadesRotas[E].EntCod + ",";
                }
                
            }


            outputTable = ObjMapsClass.Consulta_GeoCod_EntCod_Rota();
            
            
            int ID = 0;
            int TotalID = 0;

            if (outputTable.Rows.Count > 0)
            {

                ObjMapsClass = new MapsClass(); //Instancia Mapa
                
                
                
                ObjMapsClass.PontosNoCaminho = new List<MapsClass>();
                
                ObjMapsClass.TotalAtivo = outputTable.Rows.Count; //Contatos para Utilizar no JS
                TotalID = outputTable.Rows.Count - 1; //Contador de Pontos
                
                
                foreach (DataRow row in outputTable.Rows)
                {

                    ObjPontosNoCaminho = new MapsClass();//Limpa objeto a cada interação
                    
                    if(ID == 0)
                    {
                        //Endereco de Partida
                        ObjMapsClass.PartidaLatitude = Convert.ToDecimal(row["EntEnderLatitude"].ToString());
                        ObjMapsClass.PartidaLongitude = Convert.ToDecimal(row["EntEnderLongitude"].ToString());
                    }
                    else
                    {
                        if (ID == TotalID)
                            {
                                //Endereco de Destino
                                ObjMapsClass.DestinoLatitude = Convert.ToDecimal(row["EntEnderLatitude"].ToString());
                                ObjMapsClass.DestinoLongitude = Convert.ToDecimal(row["EntEnderLongitude"].ToString());
                            }
                            else
                            {
                                //Pontos no Caminho
                                ObjPontosNoCaminho.Latitude = Convert.ToDecimal(row["EntEnderLatitude"].ToString());
                                ObjPontosNoCaminho.Longitude = Convert.ToDecimal(row["EntEnderLongitude"].ToString());
                                ObjMapsClass.PontosNoCaminho.Add(ObjPontosNoCaminho);
                            
                            }
                    }

                    ID++;
                }
                
                
                
            }


            tasksList.Add(ObjMapsClass);//Adiciona a Lista para Carregar na tela
            


            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            oSerializer.MaxJsonLength = Int32.MaxValue;
            
            string sJSON = oSerializer.Serialize(tasksList);


            context.Response.Write(sJSON);
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }


}