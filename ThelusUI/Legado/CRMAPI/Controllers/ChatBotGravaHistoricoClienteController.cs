using CRMAPI.Classes;
using CRMAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRMAPI.Controllers
{
    public class ChatBotGravaHistoricoClienteController : ApiController
    {
        public HttpResponseMessage GetChatBotGravaHistoricoClienteController()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostChatBotGravaHistoricoCliente(DadosClienteChatBotModel OBJDadosClienteChatBot)
        {
            RetornoClass OBJRetorno = new RetornoClass();
            ChatBotClienteRetornoClass OBJChatBotClienteRetorno = new ChatBotClienteRetornoClass();

            try
            {
                OBJRetorno.MsgRetorno = OBJDadosClienteChatBot.IntegraHistoricoCliente();

                if (OBJRetorno.MsgRetorno == "")
                {
                    OBJChatBotClienteRetorno = OBJDadosClienteChatBot.GetDadosCliente();
                }

                var response = Request.CreateResponse<ChatBotClienteRetornoClass>(HttpStatusCode.Created, OBJChatBotClienteRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJDadosClienteChatBot.Consulta });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();
                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = OBJDadosClienteChatBot.Consulta });
                response.Headers.Location = new Uri(uri);
                return response;
            }
        }
    }
}

