using System.Data;
using System.Text;
using VendasWeb.classes;

namespace VendasWeb.WEBServiceCRM
{
    public class WSChamadoPrincipal
    {
        public int IDChamado { get; set; }

        public string Status { get; set; }

        public string Solicitante { get; set; }

        public string Abertura { get; set; }

        public string Classificacao { get; set; }

        public string Setor { get; set; }

        public string Sistema { get; set; }

        public string Prioridade { get; set; }

        public string HTMLResponsaveis { get; set; }

        public string Assunto { get; set; }

        public string Descricao { get; set; }

        public WSChamadoPrincipal RetornaChamadoPrincipal()
        {
            ChamadoClass objChamado = new ChamadoClass();

            objChamado.NumeroChamado = IDChamado;

            DataTable ChamadoPrincipalDataTable = objChamado.RecuperaDadosPrincipais();

            if (ChamadoPrincipalDataTable.Rows.Count > 0)
            {
                foreach (DataRow row in ChamadoPrincipalDataTable.Rows)
                {
                    Status = row["Status"].ToString();

                    Solicitante = row["Solicitante"].ToString();

                    Abertura = row["DataAbertura"].ToString();

                    Classificacao = row["Classificacao"].ToString();

                    Setor = row["Setor"].ToString();

                    Sistema = row["Sistema"].ToString();

                    Prioridade = row["Prioridade"].ToString();

                    HTMLResponsaveis = MontaHTMLResponsaveis(ChamadoPrincipalDataTable);

                    Assunto = row["Assunto"].ToString();

                    Descricao = row["Descricao"].ToString();

                    break;
                }
            }

            return this;
        }

        protected string MontaHTMLResponsaveis(DataTable Responsaveis)
        {
            StringBuilder HTML = new StringBuilder();

            HTML.AppendLine("<div class=\"table-responsive\"> ");

            HTML.AppendLine("   <table class=\"table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed\" ");

            HTML.AppendLine("   cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse; max-width: 100%\"> ");

            HTML.AppendLine("       <tbody> ");

            HTML.AppendLine("           <tr> ");

            HTML.AppendLine("               <th scope=\"col\">Usuário</th> ");

            HTML.AppendLine("               <th scope=\"col\">Principal</th> ");

            HTML.AppendLine("           </tr> ");

            if (Responsaveis.Rows.Count > 0)
            {
                foreach (DataRow row in Responsaveis.Rows)
                {
                    HTML.AppendLine("           <tr> ");

                    HTML.AppendLine("               <td style=\"width:90%;\"> ");

                    HTML.AppendLine("                   <span>" + row["Responsavel"].ToString() + "</span> ");

                    HTML.AppendLine("               </td> ");

                    HTML.AppendLine("               <td style=\"width:5%;\"> ");

                    HTML.AppendLine("                   <div class=\"col-xs-5 text-left checkbox\"> ");

                    {
                        if (row["Principal"].ToString() == "True")
                        {
                            HTML.AppendLine("                       <label class=\"form-checkbox form-icon active\"> ");

                            HTML.AppendLine("                           <input type=\"checkbox\" checked=\"checked\"> ");
                        }
                        else
                        {
                            HTML.AppendLine("                       <label class=\"form-checkbox form-icon\"> ");

                            HTML.AppendLine("                           <input type=\"checkbox\"> ");
                        }

                        HTML.AppendLine("                       </label> ");
                    }

                    HTML.AppendLine("                   </div> ");

                    HTML.AppendLine("               </td> ");

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