using System;
using System.Data;
using System.Text;
using VendasWeb.classes;

namespace VendasWeb.WEBServiceCRM.ClassesWEBServiceCRM
{
    public class WSConsultaCENPROTProtestos
    {
        public int IDCliente { get; set; }

        public int IDAnalise { get; set; }

        public int IDCartorio { get; set; }


        public string Cartorio { get; set; }

        public string Codigo { get; set; }

        public string Endereco { get; set; }

        public string Telefone { get; set; }

        public string Cidade { get; set; }

        public string Bairro { get; set; }

        public string Quantidade { get; set; }

        public string Total { get; set; }

        public string HTMLProtestos { get; set; }

        public WSConsultaCENPROTProtestos RetornaCENPROTProtestos()
        {
            ClienteClasse objClienteClasse = new ClienteClasse();

            objClienteClasse.IDCliente = IDCliente;

            objClienteClasse.IDAnalise = IDAnalise;

            objClienteClasse.IDCartorio = IDCartorio;

            DataTable CartorioDataTable = objClienteClasse.Consulta_CRM_CENPROT_CLIENTE_CARTORIOS();

            if (CartorioDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in CartorioDataTable.Rows)
                {
                    Cartorio = row["Cartorio"].ToString();

                    Codigo = row["Codigo"].ToString();

                    Endereco = row["Endereco"].ToString();

                    Telefone = row["Telefone"].ToString();

                    Cidade = row["Cidade"].ToString();

                    Bairro = row["Bairro"].ToString();

                    Quantidade = row["Quantidade"].ToString();

                    Total = row["Total"].ToString();

                    break;
                }
            }

            DataTable ProtestosDataTable = objClienteClasse.Consulta_CRM_CENPROT_CLIENTE_CARTORIOS_PROTESTOS();

            HTMLProtestos = MontaHTMLProtestos(ProtestosDataTable);

            return this;
        }

        protected string MontaHTMLProtestos(DataTable Protestos)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("<div class=\"table-responsive\"> ");

            HTML.AppendLine("   <table class=\"table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed\" ");

            HTML.AppendLine("   cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse; max-width: 100%\"> ");

            HTML.AppendLine("       <tbody> ");

            HTML.AppendLine("           <tr> ");

            HTML.AppendLine("               <th scope=\"col\">Data</th> ");

            //HTML.AppendLine("               <th scope=\"col\">Vencimento</th> ");

            HTML.AppendLine("               <th scope=\"col\">Valor</th> ");

            //HTML.AppendLine("               <th scope=\"col\">Chave</th> ");

            //HTML.AppendLine("               <th scope=\"col\">Apresentante</th> ");

            //HTML.AppendLine("               <th scope=\"col\">Cedente</th> ");

            //HTML.AppendLine("               <th scope=\"col\">Anuência</th> ");

            HTML.AppendLine("           </tr> ");

            if (Protestos.Rows.Count > 0)
            {
                foreach (DataRow row in Protestos.Rows)
                {
                    HTML.AppendLine("           <tr> ");

                    HTML.AppendLine("               <td style=\"width:50%;\"> ");

                    HTML.AppendLine("                   <span>" + Convert.ToDateTime(row["Data"].ToString()).ToString("dd/MM/yyyy") + "</span> ");

                    HTML.AppendLine("               </td> ");

                    //HTML.AppendLine("               <td style=\"width:5%;\"> ");

                    //HTML.AppendLine("                   <span>" + row["Vencimento"].ToString() + "</span> ");

                    //HTML.AppendLine("               </td> ");

                    HTML.AppendLine("               <td style=\"width:50%;\"> ");

                    HTML.AppendLine("                   <span>" + row["Valor"].ToString() + "</span> ");

                    HTML.AppendLine("               </td> ");

                    //HTML.AppendLine("               <td> ");

                    //HTML.AppendLine("                   <span>" + row["Chave"].ToString() + "</span> ");

                    //HTML.AppendLine("               </td> ");

                    //HTML.AppendLine("               <td> ");

                    //HTML.AppendLine("                   <span>" + row["Apresentante"].ToString() + "</span> ");

                    //HTML.AppendLine("               </td> ");

                    //HTML.AppendLine("               <td> ");

                    //HTML.AppendLine("                   <span>" + row["Cedente"].ToString() + "</span> ");

                    //HTML.AppendLine("               </td> ");

                    //HTML.AppendLine("               <td> ");

                    //HTML.AppendLine("                   <span>" + row["Anuencia"].ToString() + "</span> ");

                    //HTML.AppendLine("               </td> ");

                    HTML.AppendLine("           </tr> ");
                }
            }

            HTML.AppendLine("       </tbody> ");

            HTML.AppendLine("   </table> ");

            HTML.AppendLine("</div> ");

            return HTML.ToString();
        }
    }
}