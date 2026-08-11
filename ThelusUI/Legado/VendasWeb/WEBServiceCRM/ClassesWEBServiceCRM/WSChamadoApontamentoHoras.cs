using System.Data;
using VendasWeb.classes;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSChamadoApontamentoHoras
    {
        public int IDChamado { get; set; }

        public int IDUsuarioResponsavel { get; set; }

        public int IDApontamento { get; set; }

        public string Solicitante { get; set; }

        public string Responsavel { get; set; }

        public string DataApontamento { get; set; }

        public string NumeroHoras { get; set; }

        public string Descricao { get; set; }

        public WSChamadoApontamentoHoras RetornaChamadoApontamentoHoras()
        {
            ChamadoClass objChamado = new ChamadoClass();

            objChamado.NumeroChamado = IDChamado;

            objChamado.IDUsuarioResponsavel = IDUsuarioResponsavel;

            objChamado.IDApontamento = IDApontamento;

            DataTable ChamadoApontamentoHorasDataTable = objChamado.RecuperaDadosApontamentoHorasDetalhe();

            if (ChamadoApontamentoHorasDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ChamadoApontamentoHorasDataTable.Rows)
                {
                    Solicitante = row["Solicitante"].ToString();

                    Responsavel = row["Responsavel"].ToString();

                    DataApontamento = row["DataApontamento"].ToString();

                    NumeroHoras = row["NumeroHoras"].ToString();

                    Descricao = row["Descricao"].ToString();
                }
            }

            return this;
        }

    }
}