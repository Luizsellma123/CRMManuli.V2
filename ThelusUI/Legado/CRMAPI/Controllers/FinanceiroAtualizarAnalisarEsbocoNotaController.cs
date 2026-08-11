using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRMAPI.Models;
using CRMAPI.Classes;

namespace CRMAPI.Controllers
{
    public class FinanceiroAtualizarAnalisarEsbocoNotaController : ApiController
    {
        public HttpResponseMessage GetAllFinanceiro()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostAtualizaAnalisarEsboco(FinanceiroClass OBJFinanceiro)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = OBJFinanceiro.AtualizaAnalisarEsbocoNota();

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJFinanceiro.NumeroEsbocoSAP });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();
                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJFinanceiro.NumeroEsbocoSAP });
                response.Headers.Location = new Uri(uri);
                return response;
            }


        }
    }
}
