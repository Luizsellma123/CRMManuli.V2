using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using VendasWeb;
using VendasWeb.classes;

namespace CRMAPI.Models
{
    public class ChamadoJson
    {
        public string Data { get; set; }

        public string Solicitante { get; set; }

        public string Responsavel { get; set; }

        public string Classificacao { get; set; }

        public string Sistema { get; set; }

        public string Status { get; set; }

        public string Prioridade { get; set; }

        public string Setor { get; set; }

        public string Assunto { get; set; }

        public string Descricao { get; set; }

        ChamadoClass objChamado = new ChamadoClass();

        usuario Objusuario = new usuario();

        ExcelDataTableClass objExcelDataTableClass = new ExcelDataTableClass();

        DataTable dtVerificado = new DataTable();

        public string GravarChamado(DataTable dt)
        {
            string erro = "";

            erro = objExcelDataTableClass.Pre_Verificacao_Excel_Importacao_Chamados_DataTable(dt);

            if (erro == "") erro = VerificaCampos(dt);

            if (erro == "")
            {
                if (dtVerificado.Rows.Count > 0)
                {
                    int count = 1;

                    foreach (DataRow row in dtVerificado.Rows)
                    {
                        string erroIterativo = "";

                        objChamado = new ChamadoClass();

                        objChamado.DataChamado = Convert.ToDateTime(row["Data"].ToString());

                        objChamado.IDUsuarioOperacao =
                                Convert.ToInt32(ObterIdPorDescricao(
                                    "Tecnologia.Informação",
                                    objChamado.CarregaUsuarios(),
                                    "CodUsuario",
                                    "IDUsuario"
                                ));

                        objChamado.IDUsuarioSolicitante = Convert.ToInt32(row["IDSolicitante"].ToString());

                        objChamado.IDUsuarioResponsavel = Convert.ToInt32(row["IDResponsavel"].ToString());

                        objChamado.IDClassificacao = Convert.ToInt32(row["IDClassificacao"].ToString());

                        objChamado.IDStatus = Convert.ToInt32(row["IDStatus"].ToString());

                        objChamado.IDSistema = Convert.ToInt32(row["IDSistema"].ToString());

                        objChamado.IDPrioridade = Convert.ToInt32(row["IDPrioridade"].ToString());

                        objChamado.IDSetor = Convert.ToInt32(row["IDSetor"].ToString());

                        objChamado.IDUsuarioKeyUser = ConsultaAdmSetor(objChamado.IDSetor);

                        objChamado.Assunto = row["Assunto"].ToString();

                        objChamado.descricao = row["Descricao"].ToString();

                        erroIterativo = objChamado.GravaDadosPrincipaisChamado();

                        if (erro != "") erro += " <br> ";

                        erro += erroIterativo;

                        if (erroIterativo != "")
                        {
                            erro += $" (nó {count})";
                        }

                        count++;
                    }
                }
            }

            return erro;
        }

        private string VerificaCampos(DataTable DadosExcel)
        {
            DataTable dt = new DataTable();

            {
                dt.Columns.Add("Data");
                dt.Columns.Add("Solicitante");
                dt.Columns.Add("IDSolicitante");
                dt.Columns.Add("Responsavel");
                dt.Columns.Add("IDResponsavel");
                dt.Columns.Add("Classificacao");
                dt.Columns.Add("IDClassificacao");
                dt.Columns.Add("Sistema");
                dt.Columns.Add("IDSistema");
                dt.Columns.Add("Status");
                dt.Columns.Add("IDStatus");
                dt.Columns.Add("Prioridade");
                dt.Columns.Add("IDPrioridade");
                dt.Columns.Add("Setor");
                dt.Columns.Add("IDSetor");
                dt.Columns.Add("Assunto");
                dt.Columns.Add("Descricao");
            }

            string erro = "";

            if (DadosExcel.Rows.Count > 0)
            {
                #region Campos

                string Data = "";

                string Solicitante = "";
                string IDSolicitante = "";

                string Responsavel = "";
                string IDResponsavel = "";

                string Classificacao = "";
                string IDClassificacao = "";

                string Status = "";
                string IDStatus = "";

                string Sistema = "";
                string IDSistema = "";

                string Prioridade = "";
                string IDPrioridade = "";

                string Setor = "";
                string IDSetor = "";

                string Assunto = "";

                string Descricao = "";

                #endregion

                int count = 1;

                foreach (DataRow row in DadosExcel.Rows)
                {
                    #region Recupera campos

                    Data = row["Data"]?.ToString() ?? "";

                    Solicitante = row["Solicitante"]?.ToString() ?? "";
                    IDSolicitante = ObterIdPorDescricao(
                        Solicitante,
                        objChamado.CarregaUsuarios(),
                        "CodUsuario",
                        "IDUsuario"
                    );

                    Responsavel = row["Responsavel"]?.ToString() ?? "";
                    IDResponsavel = ObterIdPorDescricao(
                        Responsavel,
                        objChamado.CarregaUsuariosSuporte(),
                        "CodUsuario",
                        "IDUsuario"
                    );

                    Classificacao = row["Classificacao"]?.ToString() ?? "";
                    IDClassificacao = ObterIdPorDescricao(
                        Classificacao,
                        objChamado.CarregaClassificacoes(),
                        "Descricao",
                        "IDClassificacao"
                    );

                    Status = row["Status"]?.ToString() ?? "";
                    IDStatus = ObterIdPorDescricao(
                        Status,
                        objChamado.CarregaStatus(),
                        "Descricao",
                        "IDStatus"
                    );

                    Prioridade = row["Prioridade"]?.ToString() ?? "";
                    IDPrioridade = ObterIdPorDescricao(
                        Prioridade,
                        objChamado.CarregaPrioridades(),
                        "Descricao",
                        "IDPrioridade"
                    );

                    Sistema = row["Sistema"]?.ToString() ?? "";
                    IDSistema = ObterIdPorDescricao(
                        Sistema,
                        objChamado.CarregaSistemas(),
                        "Descricao",
                        "IDSistema"
                    );

                    Setor = row["Setor"]?.ToString() ?? "";
                    IDSetor = ObterIdPorDescricao(
                        Setor,
                        Objusuario.ConsultaSetoresUsuario(),
                        "Descricao",
                        "IDSetor"
                    );

                    Assunto = row["Assunto"]?.ToString() ?? "";

                    Descricao = row["Descricao"]?.ToString() ?? "";

                    #endregion

                    #region Verifica campos

                    if (Data == ""
                      || IDSolicitante == ""
                      || IDResponsavel == ""
                      || IDClassificacao == ""
                      || IDStatus == ""
                      || IDPrioridade == ""
                      || IDSistema == ""
                      || IDSetor == ""
                      || Assunto == ""
                      || Descricao == "")
                    {
                        string erroIterativo = "";

                        if (Data == "")
                            erroIterativo += "Data não preenchida.";

                        if (IDSolicitante == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O solicitante '" + Solicitante + "' não foi encontrado. " + " (nó " + count + ")";
                        }

                        if (IDResponsavel == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O responsável '" + Responsavel + "' não foi encontrado. " + " (nó " + count + ")";
                        }

                        if (IDClassificacao == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "A classificação '" + Classificacao + "' não foi encontrada." + " (nó " + count + ")";
                        }

                        if (IDStatus == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O status '" + Status + "' não foi encontrado." + " (nó " + count + ")";
                        }

                        if (IDPrioridade == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "A prioridade '" + Prioridade + "' não foi encontrada." + " (nó " + count + ")";
                        }

                        if (IDSistema == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O sistema '" + Sistema + "' não foi encontrado." + " (nó " + count + ")";
                        }

                        if (IDSetor == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "O setor '" + Setor + "' não foi encontrado." + " (nó " + count + ")";
                        }

                        if (Assunto == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "Assunto não preenchido." + " (nó " + count + ")";
                        }

                        if (Descricao == "")
                        {
                            if (erroIterativo != "") erroIterativo += " <br> ";
                            erroIterativo += "Descrição não preenchida." + " (nó " + count + ")";
                        }

                        if (erro != "" && erroIterativo != "")
                            erro += " <br> ";

                        erro += erroIterativo;

                        count++;
                    }

                    #endregion

                    if (!(Data == ""
                     || IDSolicitante == ""
                     || IDResponsavel == ""
                     || IDClassificacao == ""
                     || IDStatus == ""
                     || IDPrioridade == ""
                     || IDSistema == ""
                     || IDSetor == ""
                     || Assunto == ""
                     || Descricao == ""))
                    {
                        dt.Rows.Add(
                              Data               // Data
                            , Solicitante        // Solicitante
                            , IDSolicitante      // IDSolicitante
                            , Responsavel        // Responsavel
                            , IDResponsavel      // IDResponsavel
                            , Classificacao      // Classificacao
                            , IDClassificacao    // IDClassificacao
                            , Sistema            // Sistema
                            , IDSistema          // IDSistema
                            , Status             // Status
                            , IDStatus           // IDStatus
                            , Prioridade         // Prioridade
                            , IDPrioridade       // IDPrioridade
                            , Setor              // Setor
                            , IDSetor            // IDSetor
                            , Assunto            // Assunto
                            , Descricao          // Descricao
                        );
                    }
                }
            }

            if (erro == "") dtVerificado = dt;

            return erro;
        }

        private string ObterIdPorDescricao(string valorProcurado, DataTable tabela, string colunaDescricao, string colunaId)
        {
            if (string.IsNullOrWhiteSpace(valorProcurado))
                return "";

            if (tabela == null || tabela.Rows.Count == 0)
                return "";

            foreach (DataRow row in tabela.Rows)
            {
                if (string.Equals(valorProcurado, row[colunaDescricao]?.ToString() ?? "", StringComparison.OrdinalIgnoreCase))
                {
                    return row[colunaId]?.ToString() ?? "";
                }
            }

            return "";
        }

        public int ConsultaAdmSetor(int IDSetor)
        {
            setor objSetor = new setor();

            objSetor.IDSetor = IDSetor;

            //Grupo de Suporte
            DataTable Setores = objSetor.RetornaUsuariosSetor();

            if (Setores.Rows.Count > 0)
            {
                foreach (DataRow row in Setores.Rows)
                {
                    if (Convert.ToBoolean(row["Administrador"]))
                        return Convert.ToInt32(row["IDUsuario"]);
                }
            }

            return 0;
        }
    }
}