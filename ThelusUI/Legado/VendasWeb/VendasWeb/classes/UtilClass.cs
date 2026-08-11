
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace VendasWeb.GerencialVendas
{
    public class UtilClass
    {


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

        public String MenssagemErro(string Menssagem, bool FecharMenssagem)
        {



            string MensagemCustom = "";

            MensagemCustom = " <div class=\"alert alert-error\">";

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




    }
}