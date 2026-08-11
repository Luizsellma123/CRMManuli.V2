using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace VendasWeb.GerencialVendas
{
    public class UtilClass
    {
        #region Gerais

        public string RetornaApenasNumeros(string texto)
        {
            return Regex.Replace(texto, @"[^0-9]", "");
        }

        public string ConverteByteImageUrl(byte[] Imagem)
        {
            if (Imagem != null)
            {
                try
                {

                    //Converte Byte[] para Imagem
                    string base64String = Convert.ToBase64String(Imagem, 0, Imagem.Length);
                    return "data:image/jpeg;base64," + base64String;
                }
                catch
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }

        public string Idade_Data_Nascimento(DateTime DataNascimento)
        {

            //Data de aniversário
            //DateTime dt = Convert.ToDateTime("8/04/1984");
            DateTime dt = DataNascimento;
            //TimeSpan com a data atual menos data do niversário
            TimeSpan ts = DateTime.Today - dt;
            //Converter TimeSpan para DateTime
            //Como o new DateTime() retorna 01/01/0001 00:00:00
            //vou ter que remover um ano .AddYears(- 1) e um dia .AddDays(-1) para ter a data exata.
            DateTime idade = (new DateTime() + ts).AddYears(-1).AddDays(-1);

            //Idade em anos
            return idade.Year.ToString();

        }

        public string RecuperaDados_Select(System.Web.UI.HtmlControls.HtmlSelect Select)
        {

            string Retorno = "";

            for (int i = 0; i < Select.Items.Count; i++)
            {

                //verifica se o check ta marcado ou nao
                if (Select.Items[i].Selected == true)
                {
                    Retorno += Select.Items[i].Value + ",";
                }
            }



            return Retorno;
        }

        public bool ExisteNoList(List<string> Listlabels, string label)
        {
            if (Listlabels.Count > 0)
            {
                foreach (string labels in Listlabels)
                {
                    if (labels == label)
                        return true;
                }
            }

            return false;
        }

        public string removerAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";

            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        #endregion

        #region Menssagem

        public String MenssagemErro(string Menssagem, bool FecharMenssagem)
        {



            string MensagemCustom = "";

            MensagemCustom = " <div class=\"alert alert-danger\">";

            if (FecharMenssagem)
            {
                MensagemCustom += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button>";
            }
            //MensagemCustom += "<strong>:'( D'Oh! </strong>  " + Menssagem + "";
            MensagemCustom += "" + Menssagem + "";
            MensagemCustom += "</div>";



            return MensagemCustom;

        }

        public String MenssagemSucesso(string Menssagem, bool FecharMenssagem)
        {
            string MensagemCustom = "";

            MensagemCustom = " <div class=\"alert alert-success\">";

            if (FecharMenssagem)
            {
                MensagemCustom += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button>";
            }
            //MensagemCustom += "<strong>\\o/ Woo-hoo! </strong>  " + Menssagem + "";
            MensagemCustom += "" + Menssagem + "";
            MensagemCustom += "</div>";



            return MensagemCustom;

        }

        public String MenssagemAlerta(string Menssagem, bool FecharMenssagem)
        {

            string MensagemCustom = "";

            MensagemCustom = " <div class=\"alert alert-block\">";

            if (FecharMenssagem)
            {
                MensagemCustom += "<button type=\"button\" class=\"close\" data-dismiss=\"alert\">×</button>";
            }
            //MensagemCustom += "<strong><h4 class=\"alert-heading\">Atenção!</h4> </strong>  " + Menssagem + "";
            MensagemCustom += "<strong><h4 class=\"alert-heading\">Atenção!</h4> </strong>  " + Menssagem + "";
            MensagemCustom += "</div>";



            return MensagemCustom;

        }

        #endregion

        #region Criptografia

        #region Criptografia

        private static byte[] chave = { };

        private static byte[] iv = { 12, 34, 56, 78, 90, 102, 114, 126 };

        public string Criptografar(string valor, string chaveCriptografia)
        {
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs; byte[] input;

            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();
                input = Encoding.UTF8.GetBytes(valor); chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));

                cs = new CryptoStream(ms, des.CreateEncryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Descriptografar(string valor, string chaveCriptografia)
        {
            DESCryptoServiceProvider des;
            MemoryStream ms;
            CryptoStream cs; byte[] input;

            try
            {
                des = new DESCryptoServiceProvider();
                ms = new MemoryStream();

                input = new byte[valor.Length];
                input = Convert.FromBase64String(valor.Replace(" ", "+"));

                chave = Encoding.UTF8.GetBytes(chaveCriptografia.Substring(0, 8));

                cs = new CryptoStream(ms, des.CreateDecryptor(chave, iv), CryptoStreamMode.Write);
                cs.Write(input, 0, input.Length);
                cs.FlushFinalBlock();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region Validar CNPJ/CPF

        /// <summary>
        /// Valida o CPF ou CNPJ no CRM consultando tbm se já não esta sendo Utilizado
        /// </summary>
        /// <param name="CPF_CNPJ">CPF/CNPJ a Ser Validado</param>
        /// <param name="IDCliente">IdCliente, passar zero caso Cliente Novo</param>
        /// <return>Situacao</return>
        public string Valida_CPF_CNPJ_CRM(string CPF_CNPJ, int IDCliente, string TipoCliente)
        {

            DataTable outputTable = new DataTable();
            clsConexao ObjclsConexao = new clsConexao();

            string Retorno = "";

            try
            {

                using (SqlConnection dbConnection = new SqlConnection(ObjclsConexao.getString()))
                {

                    //Abre Conexao
                    dbConnection.Open();

                    SqlCommand dbCommand = new SqlCommand();

                    dbCommand = new SqlCommand("CRM_SP_VALIDA_CPF_CNPJ", dbConnection);

                    dbCommand.CommandType = CommandType.StoredProcedure;

                    dbCommand.Parameters.Add(new SqlParameter("@TEXTO", SqlDbType.VarChar, 30, "TEXTO"));
                    dbCommand.Parameters.Add(new SqlParameter("@IDCliente", SqlDbType.Int, 0, "IDCliente"));
                    dbCommand.Parameters.Add(new SqlParameter("@TipoCliente", SqlDbType.VarChar, 5, "TipoCliente"));


                    dbCommand.Parameters["@TEXTO"].Value = CPF_CNPJ;
                    dbCommand.Parameters["@IDCliente"].Value = IDCliente;
                    dbCommand.Parameters["@TipoCliente"].Value = TipoCliente;

                    SqlDataReader dataReader = dbCommand.ExecuteReader();
                    outputTable.Load(dataReader);
                    dataReader.Close();

                    if (outputTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in outputTable.Rows)
                        {
                            Retorno = row["Validacao"].ToString();
                        }
                    }
                    else
                    {
                        Retorno = "Erro na Validação do CPF/CNPJ";
                    }
                }
            }
            catch
            {
                Retorno = "Erro na Validação do CPF/CNPJ. Contactar o Suporte.";
            }

            return Retorno;

        }

        public bool ValidaCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return false;
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public bool ValidaCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }

        #endregion

        #region FormataCNPJCPF

        public string FormataCNPJCPF(string valor)
        {
            if (valor.Length > 11)
            {
                return this.FormatCNPJ(valor);
            }
            else
            {
                return this.FormatCPF(valor);
            }
        }

        /// <summary>
        /// Formatar uma string CNPJ
        /// </summary>
        /// <param name="CNPJ">string CNPJ sem formatacao</param>
        /// <returns>string CNPJ formatada</returns>
        /// <example>Recebe '99999999999999' Devolve '99.999.999/9999-99'</example>

        public string FormatCNPJ(string CNPJ)
        {
            return Convert.ToUInt64(CNPJ).ToString(@"00\.000\.000\/0000\-00");
        }

        /// <summary>
        /// Formatar uma string CPF
        /// </summary>
        /// <param name="CPF">string CPF sem formatacao</param>
        /// <returns>string CPF formatada</returns>
        /// <example>Recebe '99999999999' Devolve '999.999.999-99'</example>

        public string FormatCPF(string CPF)
        {
            return Convert.ToUInt64(CPF).ToString(@"000\.000\.000\-00");
        }

        /// <summary>
        /// Retira a Formatacao de uma string CNPJ/CPF
        /// </summary>
        /// <param name="Codigo">string Codigo Formatada</param>
        /// <returns>string sem formatacao</returns>
        /// <example>Recebe '99.999.999/9999-99' Devolve '99999999999999'</example>

        public string SemFormatacaoCNPJCPF(string Codigo)
        {
            return Codigo.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);
        }
        #endregion

        #region Converter List em DataTable
        public DataTable ConvertToDataTable<T>(List<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            return table;
        }
        #endregion

        #region Nomes Campos Json

        public string RemoveTraçosEspacosAcentosDosNomesCamposJson(string jsonInput)
        {
            string jsonOutput = jsonInput;

            jsonOutput = jsonOutput.Replace("\"ENDEREÇO \":", "\"ENDEREC\":");

            jsonOutput = RemoveTraçosDasExpressoes(jsonOutput);

            jsonOutput = RemoveEspacosDasExpressoes(jsonOutput);

            jsonOutput = RemoveAcentosDosNomesCamposJson(jsonOutput);

            return jsonOutput;
        }

        #region Remove traços

        public string RemoveTraçosDasExpressoes(string jsonInput)
        {
            string jsonOutput = RemoverUmTracoNomesCamposJson(jsonInput);

            jsonOutput = RemoverDoisTracosNomesCamposJson(jsonOutput);

            jsonOutput = RemoverTresTracosNomesCamposJson(jsonOutput);

            jsonOutput = RemoverUmTracoUmEspacoNomesCamposJson(jsonOutput);

            return jsonOutput;
        }

        static string RemoverUmTracoNomesCamposJson(string jsonString)
        {
            // Utilizando expressões regulares para encontrar e substituir traços nos nomes dos campos
            string padrao = @"""([\w\d]+)-([\w\d]+)"":";
            string substituicao = @"""$1$2"":";
            return Regex.Replace(jsonString, padrao, substituicao);
        }

        static string RemoverDoisTracosNomesCamposJson(string jsonString)
        {
            // Utilizando expressões regulares para encontrar e substituir traços nos nomes dos campos
            string padrao = @"""([\w\d]+)-([\w\d]+)-([\w\d]+)"":";
            string substituicao = @"""$1$2$3"":";
            return Regex.Replace(jsonString, padrao, substituicao);
        }

        static string RemoverTresTracosNomesCamposJson(string jsonString)
        {
            // Utilizando expressões regulares para encontrar e substituir traços nos nomes dos campos
            string padrao = @"""([\w\d]+)-([\w\d]+)-([\w\d]+)-([\w\d]+)"":";
            string substituicao = @"""$1$2$3$4"":";
            return Regex.Replace(jsonString, padrao, substituicao);
        }

        static string RemoverUmTracoUmEspacoNomesCamposJson(string jsonString)
        {
            // Utilizando expressões regulares para encontrar e substituir traços nos nomes dos campos
            string padrao = @"""([\w\d]+)-([\w\d]+) "":";
            string substituicao = @"""$1$2"":";
            return Regex.Replace(jsonString, padrao, substituicao);
        }

        #endregion

        #region Remove espacos

        public string RemoveEspacosDasExpressoes(string jsonInput)
        {
            string jsonOutput = RemoverUmEspacoNomesCamposJson(jsonInput);

            jsonOutput = RemoverDoisEspacosNomesCamposJson(jsonOutput);

            jsonOutput = RemoverUmEspacoAposNomesCamposJson(jsonOutput);

            return jsonOutput;
        }

        static string RemoverUmEspacoNomesCamposJson(string jsonString)
        {
            // Utilizando expressões regulares para encontrar e substituir espaços nos nomes dos campos
            string padrao = @"""([\w\d]+) ([\w\d]+)"":";
            string substituicao = @"""$1$2"":";
            return Regex.Replace(jsonString, padrao, substituicao);
        }

        static string RemoverDoisEspacosNomesCamposJson(string jsonString)
        {
            // Utilizando expressões regulares para encontrar e substituir espaços nos nomes dos campos
            string padrao = @"""([\w\d]+) ([\w\d]+) ([\w\d]+)"":";
            string substituicao = @"""$1$2$3"":";
            return Regex.Replace(jsonString, padrao, substituicao);
        }

        static string RemoverUmEspacoAposNomesCamposJson(string jsonString)
        {
            // Utilizando expressões regulares para encontrar e substituir espaços nos nomes dos campos
            string padrao = @"""([\w\d]+) "":";
            string substituicao = @"""$1"":";
            return Regex.Replace(jsonString, padrao, substituicao);
        }

        #endregion

        #region Remove acentos

        public string RemoveAcentosDosNomesCamposJson(string jsonInput)
        {
            string jsonOutput = RemoverAcentosDosNomesCamposJsonComUmTracos(jsonInput);

            return jsonOutput;
        }

        static string RemoverAcentosDosNomesCamposJsonComUmTracos(string jsonString)
        {
            // Encontra todas as expressões no formato acima
            MatchCollection matches = Regex.Matches(jsonString, @"""([\w\d]+)"":");

            // Remove os acentos de cada expressão
            foreach (Match match in matches)
            {
                string expressao = match.Groups[1].Value;
                string expressaoSemAcentos = "\"" + RemoverAcentos(expressao) + "\":";

                // Substitui a expressão original pela expressão sem acentos
                jsonString = jsonString.Replace(match.Value, expressaoSemAcentos);
            }

            return jsonString;
        }

        static string RemoverAcentos(string texto)
        {
            string textoNormalizado = texto.Normalize(NormalizationForm.FormD);
            StringBuilder resultado = new StringBuilder();

            foreach (char caracter in textoNormalizado)
            {
                UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(caracter);
                if (categoria != UnicodeCategory.NonSpacingMark)
                {
                    resultado.Append(caracter);
                }
            }

            return resultado.ToString().Normalize(NormalizationForm.FormC);
        }

        #endregion

        #endregion

        #region Datas

        public DateTime RetornaPrimeiroDiaMesAtual()
        {
            DateTime dataAtual = DateTime.Now;

            DateTime primeiroDiaMesAtual = new DateTime(dataAtual.Year, dataAtual.Month, 1);

            return primeiroDiaMesAtual;
        }

        public string FormataDataSQL(string data)
        {
            return Convert.ToDateTime(data).ToString("yyyy-MM-dd");
        }

        #endregion

        #region

        public string RetornaDataFormatada(string data, string formatoOriginal)
        {
            string dataFormatada = "";

            if (data != "")
            {
                if (formatoOriginal == "dd-MM-yyyy")
                {
                    dataFormatada = data.Substring(0, 2) + "-"
                            + data.Substring(2, 2) + "-" + data.Substring(4, 4);
                }
                else if (formatoOriginal == "yyyy-MM-dd")
                {
                    dataFormatada = data.Substring(0, 4) + "-"
                    + data.Substring(4, 2) + "-" + data.Substring(6, 2);

                    return dataFormatada;
                }

                dataFormatada = Convert.ToDateTime(dataFormatada).ToString("yyyy-MM-dd");
            }

            return dataFormatada;
        }

        #endregion

    }
}