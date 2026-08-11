using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRMAPI.Classes;
using CRMAPI.Models;

namespace CRMAPI.Controllers
{
    public class NotaAtualizarHistoricoController : ApiController
    {
        public HttpResponseMessage GetAllFinanceiro()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostAtualizaAnalisarEsboco(NotaFiscalClass OBJNota)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = OBJNota.AtualizaHistoricoNotasSAP();

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJNota.NumeroPrimarioNotaSAP });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();
                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJNota.NumeroPrimarioNotaSAP });
                response.Headers.Location = new Uri(uri);
                return response;
            }


        }
    }
}
