<%@ WebHandler Language="C#" Class="JsonMaps" %>

using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Web.SessionState;
using VendasWeb.GerencialVendas;
using System.Data;

public class JsonMaps : IHttpHandler, IRequiresSessionState
{
    /*****************************************************************************************************************************
        Json Utilizado no Maps       
     ******************************************************************************************************************************/

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        MapsClass ObjMapsClass = new MapsClass();


        List<MapsClass> tasksList = new List<MapsClass>(); 

        DataTable outputTable = new DataTable();



        if (HttpContext.Current.Session["EntCodMaps"] != null)
        {

            ObjMapsClass.UsuCod = HttpContext.Current.Session["usuario"].ToString();
            ObjMapsClass.EntCod = HttpContext.Current.Session["EntCodMaps"].ToString();
            outputTable = ObjMapsClass.Consulta_GeoCod_EntCod();

            int ID = 1;

            if (outputTable.Rows.Count > 0)
            {
                foreach (DataRow row in outputTable.Rows)
                {
                    tasksList.Add(ObjMapsClass = (new MapsClass
                    {
                        Id = ID,
                        Latitude = Convert.ToDecimal(row["EntEnderLatitude"].ToString()),
                        Longitude = Convert.ToDecimal(row["EntEnderLongitude"].ToString()),
                        Titulo = row["Titulo"].ToString(),
                        Descricao = row["Descricao"].ToString(),
                        Icon = row["icon"].ToString(),

                    }));

                    ID++;
                }
            }



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