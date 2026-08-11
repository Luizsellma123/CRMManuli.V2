using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace VendasWeb
{
    public class usuario : GerencialVendas.clsConexao
    {
        public string nome { get; set; }
        public string senha { get; set; }
        public string email { get; set; }

        public string gravaUsuario()
        {

            string strSql = "";
            Boolean teste;
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "UPDATE USUARIO SET UsuSenhaInternet='" + this.senha.ToString() + "', UsuEmail='" + this.email.ToString() + "' WHERE UsuCod='" + this.nome.ToString() + "'";

                teste = mdlFuncoes.ExecutaSQL(strSql);

                if (teste != true)
                {
                    return "Erro ao cadastrar usuário.";
                }
                else
                {
                    return "";
                }
            }
        }

        public string consultaUsuario(string Usunome)
        {
            string strSql = "";
            int cont = 0;
            funcoesBD mdlFuncoes = new funcoesBD();

            cont = this.consultaSenha(Usunome);

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();
                if (cont == 0)
                {
                    strSql = "select count(*) as CNT from USUARIO WHERE UsuCod='" + Usunome.ToString() + "'";

                    cont = (int)Convert.ToInt16(mdlFuncoes.ExecutaSqlReader(strSql));

                    if (cont <= 0)
                    {
                        return "Usuário não cadastrado.";
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "Usuário já possui senha cadastrada.";
                }
            }
        }

        public int consultaSenha(string Usunome)
        {
            string strSql = "";
            int cont = 0;
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select count(*) as CNT from USUARIO WHERE UsuCod='" + Usunome.ToString() + "' and UsuSenhaInternet<>'' ";

                cont = (int)Convert.ToInt16(mdlFuncoes.ExecutaSqlReader(strSql));
            }

            return cont;

        }

        public string consultaEmail(string Usunome)
        {
            string strSql = "";
            string email = "";
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select UsuEmail from USUARIO WHERE UsuCod='" + Usunome.ToString() + "' and UsuSenhaInternet<>'' ";

                email = (string)mdlFuncoes.ExecutaSqlReader(strSql).ToString();
            }
            return email;
        }

        public string consultaValorSenha(string Usunome)
        {
            string strSql = "";
            string senha = "";
            funcoesBD mdlFuncoes = new funcoesBD();

            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select UsuSenhaInternet from USUARIO WHERE UsuCod='" + Usunome.ToString() + "' and UsuSenhaInternet<>'' ";

                senha = mdlFuncoes.ExecutaSqlReader(strSql);
            }

            return senha;

        }

        public string consultaValorUsuario(string Usunome)
        {
            string strSql = "";
            int cont = 0;

            funcoesBD mdlFuncoes = new funcoesBD();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select count(*) as CNT from USUARIO WHERE UsuCod='" + Usunome.ToString() + "'";

                cont = (int)Convert.ToInt16(mdlFuncoes.ExecutaSqlReader(strSql));

                if (cont <= 0)
                {
                    return "Usuário não cadastrado.";
                }
                else
                {
                    return "";
                }
            }
        }


        public string ConsultaVendedorUsuario(string UsuCod)
        {
            string strSql = "";
            string VendCod = "";

            funcoesBD mdlFuncoes = new funcoesBD();
            using (SqlConnection dbConnection = new SqlConnection(strConec))
            {
                dbConnection.Open();

                strSql = "select VendCod from VENDEDOR Where UsuCod = '" + UsuCod.ToUpper() + "' and VendStat = 'Ativo' ";

                VendCod = mdlFuncoes.ExecutaSqlReader(strSql);

                return VendCod;
            }
        }

    }
}