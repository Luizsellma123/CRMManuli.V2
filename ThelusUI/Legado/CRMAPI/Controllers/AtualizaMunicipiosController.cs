using System;
using System.Net;
using CRMAPI.Models;
using CRMAPI.Classes;
using System.Web.Http;
using System.Net.Http;

namespace CRMAPI.Controllers
{
    public class AtualizaMunicipiosController : ApiController
    {
        public HttpResponseMessage GetAtualizaMunicipios()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostAtualizaMunicipios(AtualizaMunicipiosModel AtualizaMunicipios)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = AtualizaMunicipios.AtualizaMunicipios();

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);

                string uri = Url.Link("DefaultApi", new { id = AtualizaMunicipios.CodigoMunicipioSAP });

                response.Headers.Location = new Uri(uri);

                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();

                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);

                string uri = Url.Link("DefaultApi", new { id = AtualizaMunicipios.CodigoMunicipioSAP });

                response.Headers.Location = new Uri(uri);

                return response;
            }
        }
    }
}
