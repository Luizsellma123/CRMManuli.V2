using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VendasWeb.GerencialVendas;

namespace VendasWeb.classes
{
    public class LancamentoContabilClass : clsConexao
    {
        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        public string AtualizaHistoricosLancamentosContabeisAT()
        {
            string erro = "";
            string StringSQL = "";

            StringSQL = "select OJDT.TransId, 'Notas Fiscais de Saída - '+  OBTA.CodPart +' - NF-E '+convert(varchar(max),OBTA.NumDoc) Historico ";
            StringSQL += "from OBTA INNER JOIN OJDT ON OJDT.TransId = OBTA.TransId ";
            StringSQL += "where isnull(OJDT.Memo, '')= '' and OJDT.TransType = 243000003 ";
            StringSQL += "and YEAR(OJDT.RefDate) * 100 + MONTH(OJDT.RefDate) ";
            StringSQL += "in (select replace(code, '-', '') from OFPR where PeriodStat = 'N')";
            
            erro = OBJComunicacaoSAP.AtualizaLancamentoContabilHistoricoTA(StringSQL);

            return erro;
        }
    }
}