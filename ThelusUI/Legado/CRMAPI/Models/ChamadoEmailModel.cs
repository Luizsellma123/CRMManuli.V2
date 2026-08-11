using CRMAPI.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using VendasWeb;
using VendasWeb.classes;

namespace CRMAPI.Models
{
    public class ChamadoEmailModel : ConexaoClass
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        public List<ChamadoEmailAnexosModel> attachments { get; set; }

        private DebugClass OBJDebug = new DebugClass();

        private ChamadoClass objChamado = new ChamadoClass();

        private ParametroGeral objParametroGeral = new ParametroGeral();

        public string GravarChamadoEmail()
        {
            string erro = CarregaDadosDoEmailNoObj();

            if (erro == "") erro = objChamado.GravaDadosPrincipaisChamado();

            if (erro == "") erro = GravaAnexosChamadoEmail();

            if (erro != "") EnviaEmailRetornandoErro(erro);

            return erro;
        }        

        private string CarregaDadosDoEmailNoObj()
        {
            try
            {
                objChamado.IDUsuarioOperacao = RetornaIDUsuarioEmail(ExtrairEmail(this.from));

                objChamado.IDUsuarioSolicitante = objChamado.IDUsuarioOperacao;

                objChamado.IDUsuarioResponsavel = objParametroGeral.RetornaValorNumericoParametro("URESPONSAVELPADRAOCHAMADOS");

                objChamado.DataChamado = DateTime.Now.Date;

                objChamado.IDClassificacao = RetornaIDClassificacaoOutros();

                objChamado.IDStatus = RetornaIDStatusAberto();

                objChamado.IDSistema = RetornaIDSistemaOutros();

                objChamado.IDPrioridade = RetornaIDPrioridadeBaixa();

                objChamado.IDSetor = RetornaSetorUsuario(objChamado.IDUsuarioOperacao);

                objChamado.IDUsuarioKeyUser = ConsultaAdmSetor(objChamado.IDSetor);

                objChamado.Assunto = this.subject;                

                objChamado.descricao = this.body;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "";
        }               

        private string GravaAnexosChamadoEmail()
        {
            string erro = "";

            try
            {
                if (attachments != null && attachments.Count > 0)
                {
                    foreach (ChamadoEmailAnexosModel anexo in attachments)
                    {
                        byte[] fileBytes = Convert.FromBase64String(anexo.content);

                        erro = objChamado.GravaArquivoServidor(fileBytes, anexo.filename, anexo.contentType);

                        objChamado.DescricaoArquivo = anexo.filename;

                        if (erro == "") erro = objChamado.GravaDadosAnexosChamado();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return erro;
        }

        private int RetornaIDUsuarioEmail(string email)
        {
            usuario objusuario = new usuario();

            objusuario.email = email;

            int IDUsuario = objusuario.RecuperaUsuarioEmail();

            if (IDUsuario == 0) throw new Exception("Nenhum usuário cadastrado com este email." +
                "<br> Peça para seu supervisor abrir um chamado para cadastro.");

            return IDUsuario;
        }

        private string ExtrairEmail(string input)
        {
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

        private int RetornaIDClassificacaoOutros()
        {
            DataTable Classificacao = objChamado.CarregaClassificacoes();

            if (Classificacao.Rows.Count > 0)
            {
                foreach (DataRow row in Classificacao.Rows)
                {
                    if (row["Descricao"].ToString() == "Outros")
                        return Convert.ToInt32(row["IDClassificacao"]);
                }
            }

            return 0;
        }

        private int RetornaIDStatusAberto()
        {
            DataTable Status = objChamado.CarregaStatus();

            if (Status.Rows.Count > 0)
            {
                foreach (DataRow row in Status.Rows)
                {
                    if (row["Descricao"].ToString() == "Aberto")
                        return Convert.ToInt32(row["IDStatus"]);
                }
            }

            return 0;
        }

        private int RetornaIDSistemaOutros()
        {
            DataTable Sistemas = objChamado.CarregaSistemas();

            if (Sistemas.Rows.Count > 0)
            {
                foreach (DataRow row in Sistemas.Rows)
                {
                    if (row["Descricao"].ToString() == "Outros")
                        return Convert.ToInt32(row["IDSistema"]);
                }
            }

            return 0;
        }

        private int RetornaIDPrioridadeBaixa()
        {
            DataTable Sistemas = objChamado.CarregaPrioridades();

            if (Sistemas.Rows.Count > 0)
            {
                foreach (DataRow row in Sistemas.Rows)
                {
                    if (row["Descricao"].ToString() == "Baixa")
                        return Convert.ToInt32(row["IDPrioridade"]);
                }
            }

            return 0;
        }

        private int RetornaSetorUsuario(int IDUsuario)
        {
            usuario objUsuario = new usuario();

            objUsuario.IDUsuario = IDUsuario;

            DataTable SetorUsuario = objUsuario.ConsultaSetoresUsuario();

            if (SetorUsuario.Rows.Count > 0)
            {
                foreach (DataRow row in SetorUsuario.Rows)
                {
                    return Convert.ToInt32(row["IDSetor"]);
                }
            }

            return 0;
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

        public void EnviaEmailRetornandoErro(string erro)
        {
            objChamado.EmailPara = ExtrairEmail(this.from);

            objChamado.Assunto = "Erro ao abrir chamado via e-mail";

            objChamado.DescricaoEmail = objChamado.EmailPara + "<br>" + erro;

            objChamado.CodigoUsuario = "API CRM";

            objChamado.EnviaEmail();
        }

    }
}