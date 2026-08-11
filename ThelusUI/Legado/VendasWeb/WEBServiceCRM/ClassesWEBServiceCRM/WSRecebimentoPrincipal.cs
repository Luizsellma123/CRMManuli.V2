using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VendasWeb.classes;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSRecebimentoPrincipal
    {
        public int IDEmpresa { get; set; }

        public int IDRecebimento { get; set; }

        public string Empresa { get; set; }

        public string Responsavel { get; set; }

        public string Situacao { get; set; }

        public string Setor { get; set; }

        public string CNPJ { get; set; }

        public string Fornecedor { get; set; }

        public string NF { get; set; }

        public string DataRecebimento { get; set; }

        public string Observacao { get; set; }

        public WSRecebimentoPrincipal RetornaRecebimentoPrincipal()
        {
            RecebimentoClass objRecebimento = new RecebimentoClass();

            objRecebimento.IDEmpresa = IDEmpresa;

            objRecebimento.IDRecebimento = IDRecebimento;

            DataTable RecebimentoPrincipalDataTable = objRecebimento.ConsultaRecebimento();

            if (RecebimentoPrincipalDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in RecebimentoPrincipalDataTable.Rows)
                {
                    Empresa = row["Empresa"].ToString();

                    Responsavel = row["Responsavel"].ToString();

                    DataRecebimento = row["DataRecebimento"].ToString();

                    Situacao = row["Situacao"].ToString();

                    Setor = row["Setor"].ToString();

                    CNPJ = row["CNPJ"].ToString();

                    Fornecedor = row["Fornecedor"].ToString();

                    NF = row["NF"].ToString();

                    Observacao = row["Observacao"].ToString();

                    break;
                }
            }

            return this;
        }
    }
}