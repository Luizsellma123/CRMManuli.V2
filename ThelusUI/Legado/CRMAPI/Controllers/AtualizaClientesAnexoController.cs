using System;
using System.Net;
using CRMAPI.Models;
using CRMAPI.Classes;
using System.Web.Http;
using System.Net.Http;

namespace CRMAPI.Controllers
{
    public class AtualizaClientesAnexoController : ApiController
    {
        public HttpResponseMessage GetAtualizaClientesAnexo()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostAtualizaClientesAnexo(AtualizaClientesAnexoModel objAtualizaClientesAnexo)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = objAtualizaClientesAnexo.AtualizaClientesAnexo();

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);

                string uri = Url.Link("DefaultApi", new { id = objAtualizaClientesAnexo.CodigoSAP });

                response.Headers.Location = new Uri(uri);

                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();

                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);

                string uri = Url.Link("DefaultApi", new { id = objAtualizaClientesAnexo.CodigoSAP });

                response.Headers.Location = new Uri(uri);

                return response;
            }
        }
    }
}
