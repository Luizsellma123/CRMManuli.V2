using System;
using System.Net;
using CRMAPI.Models;
using CRMAPI.Classes;
using System.Net.Http;
using System.Web.Http;

namespace CRMAPI.Controllers
{
    public class AvisaChamadoAprovacaoController : ApiController
    {
        public HttpResponseMessage GetFinalizaChamado()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostFinalizaChamado()
        {
            RetornoClass OBJRetorno = new RetornoClass();

            AvisaChamadoAprovacaoModel OBJAvisaChamadoAprovacaoModel = new AvisaChamadoAprovacaoModel();

            try
            {
                OBJRetorno.MsgRetorno = OBJAvisaChamadoAprovacaoModel.AvisaAprovacaoChamados();

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJAvisaChamadoAprovacaoModel.IDChamado });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();

                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJAvisaChamadoAprovacaoModel.IDChamado });
                response.Headers.Location = new Uri(uri);
                return response;
            }

        }
    }
}
