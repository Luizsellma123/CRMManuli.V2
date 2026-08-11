using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRMAPI.Classes;

namespace CRMAPI.Models
{
    public class GeraPosicaoDiariaModel
    {
        public string IDUsuario { get; set; }

        public string PeriodoInicial { get; set; }

        public string PeriodoFinal { get; set; }

        public string Automatico { get; set; }

        public void GravaDadosPosicaoDiaria(RetornoClass OBJRetorno)
        {
            try
            {
                this.PeriodoFinal = Convert.ToDateTime(this.PeriodoFinal).AddDays(-1).ToString("dd-MM-yyyy");

                PosicaoDiariaClass objPosicaoDiariaClass = new PosicaoDiariaClass(this);

                objPosicaoDiariaClass.CRM_SP_GRAVA_POSICAO_DIARIA();

                OBJRetorno.JSONRetorno = MontaJsonRetorno(objPosicaoDiariaClass.GetIDPosicaoDiaria());

                objPosicaoDiariaClass.CRM_SP_GRAVA_POSICAO_DIARIA_FATURADOS();

                objPosicaoDiariaClass.CRM_SP_GRAVA_POSICAO_DIARIA_DEVOLUCOES();

                objPosicaoDiariaClass.CRM_SP_GRAVA_POSICAO_DIARIA_PENDENTES();

                objPosicaoDiariaClass.CRM_SP_GRAVA_POSICAO_DIARIA_ESTRATIFICACAO();

                objPosicaoDiariaClass.CRM_SP_GRAVA_POSICAO_DIARIA_ESTRATIFICACAO_BACKLOG();

                objPosicaoDiariaClass.EnviaEmail();
            }
            catch (Exception ex)
            {
                OBJRetorno.MsgRetorno = ex.Message;
            }
        }

        private string MontaJsonRetorno(int IDPosicaoDiaria)
        {
            return "{ \"IDPosicaoDiaria\": " + IDPosicaoDiaria + " }";
        }
    }
}