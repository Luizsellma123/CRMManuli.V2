using System;
using System.Net;
using CRMAPI.Models;
using CRMAPI.Classes;
using System.Web.Http;
using System.Net.Http;

namespace CRMAPI.Controllers
{
    public class AtualizaNotasFiscaisPedidoController : ApiController
    {
        public HttpResponseMessage GetAtualizaNotasFiscaisPedido()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostAtualizaNotasFiscaisPedido(AtualizaNotasFiscaisPedidoModel objAtualizaNotasFiscaisPedido)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = objAtualizaNotasFiscaisPedido.AtualizaNotasFiscaisPedido();

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);

                string uri = Url.Link("DefaultApi", new { id = objAtualizaNotasFiscaisPedido.NumeroPedidoSAP });

                response.Headers.Location = new Uri(uri);

                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();

                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);

                string uri = Url.Link("DefaultApi", new { id = objAtualizaNotasFiscaisPedido.NumeroPedidoSAP });

                response.Headers.Location = new Uri(uri);

                return response;
            }
        }
    }
}
