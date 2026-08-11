using System;
using System.Net;
using CRMAPI.Models;
using CRMAPI.Classes;
using System.Web.Http;
using System.Net.Http;

namespace CRMAPI.Controllers
{
    public class AtualizaClienteIdentificacaoFiscalController : ApiController
    {
        public HttpResponseMessage GetAtualizaClienteIdentificacaoFiscal()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostAtualizaClienteIdentificacaoFiscal(AtualizaClienteIdentificacaoFiscalModel objAtualizaClienteIdentificacaoFiscal)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = objAtualizaClienteIdentificacaoFiscal.AtualizaClientesIdentificacaoFiscal();

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);

                string uri = Url.Link("DefaultApi", new { id = objAtualizaClienteIdentificacaoFiscal.CodigoClienteSAP });

                response.Headers.Location = new Uri(uri);

                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();

                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);

                string uri = Url.Link("DefaultApi", new { id = objAtualizaClienteIdentificacaoFiscal.CodigoClienteSAP });

                response.Headers.Location = new Uri(uri);

                return response;
            }
        }
    }
}
