using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace CRMAPI.Classes
{
    public class UtilClass
    {
        public List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public string RetornaApenasDigitos(string string_)
        {
            return new string(string_.Where(char.IsDigit).ToArray());
        }

        public string RetornaApenasLetrasENumeros(string entrada)
        {
            return new string(entrada.Where(char.IsLetterOrDigit).ToArray());
        }

        public string RemoveCaracteresInvalidosParaArquivo(string entrada)
        {
            var caracteresInvalidos = Path.GetInvalidFileNameChars();
            return new string(entrada.Where(c => !caracteresInvalidos.Contains(c)).ToArray());
        }

        public string FormataCPF(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray()); // Remove tudo que não é dígito
            if (cpf.Length == 11)
            {
                return string.Format("{0}.{1}.{2}-{3}",
                    cpf.Substring(0, 3),   // XXX
                    cpf.Substring(3, 3),   // XXX
                    cpf.Substring(6, 3),   // XXX
                    cpf.Substring(9, 2));  // XX
            }
            return cpf; // Retorna o valor original se não tiver 11 dígitos
        }

        public string FormataCNPJ(string cnpj)
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray()); // Remove tudo que não é dígito
            if (cnpj.Length == 14)
            {
                return string.Format("{0}.{1}.{2}/{3}-{4}",
                    cnpj.Substring(0, 2),   // XX
                    cnpj.Substring(2, 3),   // XXX
                    cnpj.Substring(5, 3),   // XXX
                    cnpj.Substring(8, 4),   // XXXX
                    cnpj.Substring(12, 2)); // XX
            }
            return cnpj; // Retorna o valor original se não tiver 14 dígitos
        }

        public string FormataCEP(string cep)
        {
            cep = new string(cep.Where(char.IsDigit).ToArray()); // Remove tudo que não é dígito
            if (cep.Length == 8)
            {
                return string.Format("{0}-{1}", cep.Substring(0, 5), cep.Substring(5, 3));
            }
            return cep; // Retorna o valor original se não tiver 8 dígitos
        }

        public string FormataTelefone(string telefone)
        {
            telefone = new string(telefone.Where(char.IsDigit).ToArray()); // Remove tudo que não é dígito
            if (telefone.Length == 10)
            {
                return string.Format("({0}) {1}-{2}", telefone.Substring(0, 2), telefone.Substring(2, 4), telefone.Substring(6, 4));
            }
            return telefone; // Retorna o valor original se não tiver 10 dígitos
        }

        public string FormataCelular(string celular)
        {
            celular = new string(celular.Where(char.IsDigit).ToArray()); // Remove tudo que não é dígito
            if (celular.Length == 11)
            {
                return string.Format("({0}) {1} {2}-{3}", celular.Substring(0, 2), celular.Substring(2, 1), celular.Substring(3, 4), celular.Substring(7, 4));
            }
            return celular; // Retorna o valor original se não tiver 11 dígitos
        }

        public string ExtrairEmail(string input)
        {
            input = input.Replace("mailto:", "");

            // Definindo a expressão regular para capturar o email
            string pattern = @"<(?<email>[^>]+)>";

            // Criando a expressão regular
            Regex regex = new Regex(pattern);

            // Tentando encontrar uma correspondência na string de entrada
            Match match = regex.Match(input);

            // Se encontrar uma correspondência, retorna o email, caso contrário retorna null
            if (match.Success)
            {
                return match.Groups["email"].Value;
            }

            return null;
        }

        public bool ValidarEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            if (!email.Contains("."))
                return false;

            int teste = email.LastIndexOf(".");

            if (email.LastIndexOf(".") == email.Length - 1)
                return false;

            int arrobaIndex = email.IndexOf('@');

            // Verifica se o arroba existe e não está no início nem no final do e-mail
            return arrobaIndex > 0 && arrobaIndex < email.Length - 1;
        }

        public string ValidarSenha(string senha)
        {
            // Verifica se a senha tem pelo menos 3 caracteres para evitar sequências curtas
            if (senha.Length < 3)
            {
                return "A senha deve ter pelo menos 3 caracteres.";
            }

            // Verificar por sequências numéricas e de letras
            for (int i = 0; i < senha.Length - 2; i++)
            {
                // Verificar sequência de números
                if (char.IsDigit(senha[i]) && char.IsDigit(senha[i + 1]) && char.IsDigit(senha[i + 2]))
                {
                    int primeiroNumero = senha[i] - '0';
                    int segundoNumero = senha[i + 1] - '0';
                    int terceiroNumero = senha[i + 2] - '0';

                    if (segundoNumero == primeiroNumero + 1 && terceiroNumero == segundoNumero + 1)
                    {
                        return "A senha contém uma sequência numérica.";
                    }
                }

                // Verificar sequência de letras
                if (char.IsLetter(senha[i]) && char.IsLetter(senha[i + 1]) && char.IsLetter(senha[i + 2]))
                {
                    char primeiraLetra = senha[i];
                    char segundaLetra = senha[i + 1];
                    char terceiraLetra = senha[i + 2];

                    if (segundaLetra == primeiraLetra + 1 && terceiraLetra == segundaLetra + 1)
                    {
                        return "A senha contém uma sequência de letras.";
                    }
                }
            }

            return ""; // Senha válida
        }

        public bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = cpf.Replace(".", "").Replace("-", "");

            // Complementa com zeros à esquerda se tiver menos de 11 dígitos
            cpf = cpf.PadLeft(11, '0');

            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                return false;

            // Verifica se todos os dígitos são iguais, o que tornaria o CPF inválido
            if (cpf.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += (tempCpf[i] - '0') * multiplicador1[i];

            int resto = soma % 11;
            int primeiroDigito = resto < 2 ? 0 : 11 - resto;

            tempCpf += primeiroDigito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += (tempCpf[i] - '0') * multiplicador2[i];

            resto = soma % 11;
            int segundoDigito = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(primeiroDigito.ToString() + segundoDigito.ToString());
        }

        public bool ValidarCNPJ(string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
                return false;

            cnpj = cnpj.Replace(".", "")
                       .Replace("/", "")
                       .Replace("-", "");

            // Complementa com zeros à esquerda se tiver menos de 14 dígitos
            cnpj = cnpj.PadLeft(14, '0');

            if (cnpj.Length != 14 || !cnpj.All(char.IsDigit))
                return false;

            // Verifica se todos os dígitos são iguais
            if (cnpj.Distinct().Count() == 1)
                return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += (tempCnpj[i] - '0') * multiplicador1[i];

            int resto = soma % 11;
            int primeiroDigito = resto < 2 ? 0 : 11 - resto;

            tempCnpj += primeiroDigito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += (tempCnpj[i] - '0') * multiplicador2[i];

            resto = soma % 11;
            int segundoDigito = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith(primeiroDigito.ToString() + segundoDigito.ToString());
        }

        public string FormataDataSQL(string data)
        {
           return Convert.ToDateTime(data).ToString("yyyy-MM-dd");
        }
    }
}