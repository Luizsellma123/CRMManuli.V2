using System;
using System.Net;
using CRMAPI.Models;
using CRMAPI.Classes;
using System.Net.Http;
using System.Web.Http;

namespace CRMAPI.Controllers
{
    public class GeraChamadoController : ApiController
    {
        public HttpResponseMessage GetGeraChamado()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostGeraChamado(ChamadoEmailModel OBJChamado)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = OBJChamado.GravarChamadoEmail();

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJChamado.to });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();

                OBJChamado.EnviaEmailRetornandoErro(OBJRetorno.MsgRetorno);

                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJChamado.to });
                response.Headers.Location = new Uri(uri);
                return response;
            }

        }
    }
}
