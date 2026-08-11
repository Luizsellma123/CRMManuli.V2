using System;
using System.Collections.Generic;
using System.Linq;
using VendasWeb.classes;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;

namespace VendasWeb.SQL
{
    public class Atualizar : GerencialVendas.clsConexao
    {
        ComunicacaoSAPClass OBJComunicacaoSAP = new ComunicacaoSAPClass();

        string StringSQL = "";

        string nomeProcedureUltimo = "";
        bool existeProcedureUltimo;
        string procedureFormatadaUltimo = "";

        List<string> procedures;

        public string AtualizarStoredProcedures()
        {

            StringBuilder erroStringBuilder = new StringBuilder();
            string erro = "";

            try
            {
                string diretorioBase = AppDomain.CurrentDomain.BaseDirectory;
                string caminhoStoredProcedures = Path.Combine(diretorioBase, "SQL", "StoredProcedures");

                procedures = Directory.GetFiles(caminhoStoredProcedures).ToList();

                if (procedures.Count > 0)
                {
                    erro = VerificaProceduresDuplicadas();

                    if (erro == "")
                    {
                        foreach (string procedure in procedures)
                        {
                            try
                            {
                                ExecutaProcedure(procedure);
                            }
                            catch (Exception ex)
                            {
                                erroStringBuilder.AppendLine(ex.Message);

                                erroStringBuilder.AppendLine(nomeProcedureUltimo == "" ? "" : "<br> Stored Procedure: " + nomeProcedureUltimo + "<br><br>");
                            }
                        }

                        if (erroStringBuilder.ToString().Trim() != "") throw new Exception(erroStringBuilder.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                erro = ex.Message;
            }

            return erro;
        }

        private string VerificaProceduresDuplicadas()
        {
            List<string> nomeProcedures = new List<string>();

            StringBuilder erro = new StringBuilder("");

            erro.AppendLine("A(s) seguinte(s) procedure(s) está(ão) repetida(s): <br>");

            foreach (string procedure in procedures)
            {
                StringSQL = File.ReadAllText(procedure);

                arrumaStringSQL();

                string nomeProcedure = RetornaNomeProcedure();

                if (nomeProcedures.Contains(nomeProcedure))
                {
                    erro.AppendLine(nomeProcedure + "<br>");
                }
                else
                {
                    nomeProcedures.Add(nomeProcedure);
                }
            }

            if (erro.ToString().Trim() == "A(s) seguinte(s) procedure(s) está(ão) repetida(s): <br>")
                return "";

            return erro.ToString();
        }

        private void ExecutaProcedure(string caminhoProcedure)
        {
            StringSQL = File.ReadAllText(caminhoProcedure);

            arrumaStringSQL();

            string nomeProcedure = RetornaNomeProcedure();

            nomeProcedureUltimo = nomeProcedure;

            bool existeProcedure = VerificaExistenciaStoredProcedure(nomeProcedure);

            existeProcedureUltimo = existeProcedure;

            string procedureFormatada = FormataStoredProcedure(!existeProcedure);

            procedureFormatadaUltimo = procedureFormatada;

            ExecutaProcedureSQL(procedureFormatada);

            //ExecutaProcedureSQL(FormataStoredProcedure(caminhoProcedure, !VerificaExistenciaStoredProcedure(RetornaNomeProcedure(caminhoProcedure))));
        }

        private void arrumaStringSQL()
        {
            AtualizaPalavraChave("CREATE PROCEDURE", "CREATE PROCEDURE");
            AtualizaPalavraChave("ALTER PROCEDURE", "ALTER PROCEDURE");
        }

        private void AtualizaPalavraChave(string palavraChaveOriginal, string novaPalavraChave, int rep = 1)
        {
            string padrao = @"\b" + Regex.Escape(palavraChaveOriginal) + @"\s*\b";
            Regex regex = new Regex(padrao, RegexOptions.IgnoreCase);

            if (regex.IsMatch(StringSQL))
            {
                StringSQL = regex.Replace(StringSQL, novaPalavraChave);
            }
            else
            {
                if (rep == 1)
                    AtualizaPalavraChave(palavraChaveOriginal.Replace(" ", "  "), novaPalavraChave, rep + 1);
                else if (rep == 2)
                    AtualizaPalavraChave(palavraChaveOriginal.Replace(" ", "   "), novaPalavraChave, rep + 1);
            }
        }

        //private void arrumaStringSQL()
        //{
        //    string create = "CREATE PROCEDURE";

        //    if (StringSQL.Contains("create procedure"))
        //        StringSQL = StringSQL.Replace("create procedure", create);
        //    else if (StringSQL.Contains("create  procedure"))
        //        StringSQL = StringSQL.Replace("create  procedure", create);
        //    else if (StringSQL.Contains("CREATE procedure"))
        //        StringSQL = StringSQL.Replace("CREATE procedure", create);
        //    else if (StringSQL.Contains("CREATE  PROCEDURE"))
        //        StringSQL = StringSQL.Replace("CREATE  PROCEDURE", create);
        //    else if (StringSQL.Contains("CREATE  procedure"))
        //        StringSQL = StringSQL.Replace("CREATE  procedure", create);

        //    string alter = "ALTER PROCEDURE";

        //    if (StringSQL.Contains("alter procedure"))
        //        StringSQL = StringSQL.Replace("alter procedure", alter);
        //    else if (StringSQL.Contains("alter  procedure"))
        //        StringSQL = StringSQL.Replace("alter  procedure", alter);
        //    else if (StringSQL.Contains("ALTER procedure"))
        //        StringSQL = StringSQL.Replace("ALTER procedure", alter);
        //    else if (StringSQL.Contains("ALTER  PROCEDURE"))
        //        StringSQL = StringSQL.Replace("ALTER  PROCEDURE", alter);
        //    else if (StringSQL.Contains("alter PROCEDURE"))
        //        StringSQL = StringSQL.Replace("alter PROCEDURE", alter);
        //}

        private string RetornaNomeProcedure()
        {
            string padrao = @"ALTER\s+PROCEDURE\s+\[([^]]+)\]\.\[([^]]+)\]";

            Match match = Regex.Match(StringSQL, padrao);

            if (match.Success)
                return match.Groups[2].Value;

            padrao = @"CREATE\s+PROCEDURE\s+\[([^]]+)\]\.\[([^]]+)\]";

            match = Regex.Match(StringSQL, padrao);

            if (match.Success)
                return match.Groups[2].Value;

            return "";
        }

        private bool VerificaExistenciaStoredProcedure(string nomeProcedure)
        {
            StringBuilder StringSQL = new StringBuilder();

            StringSQL.AppendLine("SELECT * FROM sysobjects ");
            StringSQL.AppendLine("WHERE  id = object_id(N'[dbo].[" + nomeProcedure + "]') ");
            StringSQL.AppendLine("and OBJECTPROPERTY(id, N'IsProcedure') = 1 ");

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                using (SqlCommand dbCommand = new SqlCommand(StringSQL.ToString(), dbConnection))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    //Deixa o Timeout da consulta com cerca de 4 minutos
                    dbCommand.CommandTimeout = 340;

                    DataTable outputTable = new DataTable();

                    using (SqlDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        outputTable.Load(dataReader);
                    }

                    return (outputTable.Rows.Count > 0);
                }
            }
        }

        private string FormataStoredProcedure(bool criar)
        {
            int IndexALTERprocedure = 0;

            if (criar)
            {
                StringSQL = StringSQL.Replace("ALTER PROCEDURE", "CREATE PROCEDURE");

                IndexALTERprocedure = StringSQL.IndexOf("CREATE PROCEDURE");
            }
            else
            {
                StringSQL = StringSQL.Replace("CREATE PROCEDURE", "ALTER PROCEDURE");

                IndexALTERprocedure = StringSQL.IndexOf("ALTER PROCEDURE");
            }

            if (IndexALTERprocedure < 0)
            {
                int teste = 1;
            }

            StringSQL = StringSQL.Substring(IndexALTERprocedure);

            StringSQL = StringSQL.Replace("\r\nGO", "");

            return StringSQL;
        }

        private void ExecutaProcedureSQL(string StringSQL)
        {
            //int teste = Convert.ToInt32("teste");

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                using (SqlCommand dbCommand = new SqlCommand(StringSQL, dbConnection))
                {
                    //Abre Conexao
                    dbConnection.Open();

                    //Deixa o Timeout da consulta com cerca de 4 minutos
                    dbCommand.CommandTimeout = 340;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                }
            }
        }

    }
}