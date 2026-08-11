using System;
using System.Net;
using CRMAPI.Models;
using CRMAPI.Classes;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using System.Data;

namespace CRMAPI.Controllers
{
    public class GeraChamadoJsonController : ApiController
    {
        public HttpResponseMessage GetGeraChamadoJson()
        {
            return Request.CreateErrorResponse(HttpStatusCode.Created, "Nada a retornar");
        }

        public HttpResponseMessage PostGeraChamadoJson(List<ChamadoJson> ListobjChamadoJson)
        {
            RetornoClass OBJRetorno = new RetornoClass();

            try
            {
                string erro = string.Empty;

                DataTable dt = new DataTable();

                {
                    dt.Columns.Add("Data");
                    dt.Columns.Add("Solicitante");
                    dt.Columns.Add("Responsavel");
                    dt.Columns.Add("Classificacao");
                    dt.Columns.Add("Sistema");
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Prioridade");
                    dt.Columns.Add("Setor");
                    dt.Columns.Add("Assunto");
                    dt.Columns.Add("Descricao");
                }

                foreach (ChamadoJson chamado in ListobjChamadoJson)
                {                   
                    DataRow Row = dt.NewRow();

                    {
                        Row["Data"] = chamado.Data;
                        Row["Solicitante"] = chamado.Solicitante;
                        Row["Responsavel"] = chamado.Responsavel;
                        Row["Classificacao"] = chamado.Classificacao;
                        Row["Sistema"] = chamado.Sistema;
                        Row["Status"] = chamado.Status;
                        Row["Prioridade"] = chamado.Prioridade;
                        Row["Setor"] = chamado.Setor;
                        Row["Assunto"] = chamado.Assunto;
                        Row["Descricao"] = chamado.Descricao;
                    }

                  dt.Rows.Add(Row);
                }

                ChamadoJson objChamadoJson = new ChamadoJson();

                OBJRetorno.MsgRetorno = objChamadoJson.GravarChamado(dt);

                var response = Request.CreateResponse<RetornoClass>(HttpStatusCode.Created, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = ListobjChamadoJson.Count });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message.ToString();

                var response = Request.CreateResponse(HttpStatusCode.Forbidden, OBJRetorno);
                string uri = Url.Link("DefaultApi", new { id = ListobjChamadoJson.Count });
                response.Headers.Location = new Uri(uri);
                return response;
            }
        }
    }
}