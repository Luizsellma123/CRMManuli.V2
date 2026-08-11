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
    public class FinanceiroCancelarPedidosPeriodoController : ApiController
    {
        public HttpResponseMessage GetAllFinanceiroCancelarPeriodoPedidos()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostFinanceiroCancelarPedidosPeriodo()
        {
            RetornoClass OBJRetorno = new RetornoClass();
            FinanceiroClass OBJFinanceiro = new FinanceiroClass();

            try
            {
                OBJRetorno.MsgRetorno = OBJFinanceiro.CancelaPedidosPeriodoCRM();

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
