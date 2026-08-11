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
    public class RecuperaDadosSerasaController : ApiController
    {

        public HttpResponseMessage GetAllRecuperaDadosReceita()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostRecuperaDadosReceita(DadosSerasaModel OBJDadosSerasa)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = OBJDadosSerasa.ConsultaDadosSerasa();

                if (OBJRetorno.MsgRetorno == "")
                {
                    OBJRetorno.JSONRetorno = OBJDadosSerasa.GetJSONRetorno();
                }

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJDadosSerasa.NumeroDocumento });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();
                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJDadosSerasa.NumeroDocumento });
                response.Headers.Location = new Uri(uri);
                return response;
            }
        }
    }
}
