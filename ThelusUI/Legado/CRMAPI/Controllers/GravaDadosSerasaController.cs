using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRMAPI.Models;
using VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM;

namespace CRMAPI.Controllers
{
    public class GravaDadosSerasaController : ApiController
    {
        public HttpResponseMessage GetGravaDadosSerasa()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostGravaDadosSerasa(WSRecuperaDadosSerasa objWSRecuperaDadosSerasa)
        {
            WSRetornoJSONClass objWSRetornoJSONClass = new WSRetornoJSONClass();

            GravaDadosSerasaModel objGravaDadosSerasaModel = new GravaDadosSerasaModel();

            try
            {
                objWSRetornoJSONClass.MsgRetorno = objGravaDadosSerasaModel.GravaDadosSerasa(objWSRecuperaDadosSerasa);

                var response = Request.CreateResponse<WSRetornoJSONClass>(HttpStatusCode.Created, objWSRetornoJSONClass);

                string uri = Url.Link("DefaultApi", new { id = objWSRecuperaDadosSerasa.TipoConsulta });

                response.Headers.Location = new Uri(uri);

                return response;
            }
            catch (Exception ex)
            {
                objWSRetornoJSONClass.MsgRetorno = ex.Message.ToString();

                var response = Request.CreateResponse(HttpStatusCode.Forbidden, objWSRetornoJSONClass);

                string uri = Url.Link("DefaultApi", new { id = objWSRecuperaDadosSerasa.TipoConsulta });

                response.Headers.Location = new Uri(uri);

                return response;
            }

        }
    }
}
