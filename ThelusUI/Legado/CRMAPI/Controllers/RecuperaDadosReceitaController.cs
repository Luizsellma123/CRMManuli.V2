using CRMAPI.Classes;
using CRMAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRMAPI.Controllers
{
    public class RecuperaDadosReceitaController : ApiController
    {
        public HttpResponseMessage GetAllRecuperaDadosReceita()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostRecuperaDadosReceita(DadosReceitaModel OBJDadosReceita)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = OBJDadosReceita.ConsultaDocumentos();

                if(OBJRetorno.MsgRetorno == "")
                {
                    OBJRetorno.JSONRetorno = OBJDadosReceita.GetDadosReceita();
                }

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJDadosReceita.NumeroDocumento });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();
                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJDadosReceita.NumeroDocumento });
                response.Headers.Location = new Uri(uri);
                return response;
            }


        }
    }
}
