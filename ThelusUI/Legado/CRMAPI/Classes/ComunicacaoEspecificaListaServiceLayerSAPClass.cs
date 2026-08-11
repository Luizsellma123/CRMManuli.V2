using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRMAPI.Classes
{
    public class ComunicacaoEspecificaListaServiceLayerSAPClass
    {
        public List<ComunicacaoEspecificaServiceLayerSAPClass> OBJUsuarios { get; set; } = new List<ComunicacaoEspecificaServiceLayerSAPClass>();
        public string UsuarioAcessoSAP { get; set; }
        public string SenhaUsuarioAcessoSAP { get; set; }

        public string ConectaUsuario()
        {
            string erro = "";

            try
            {
                // Verifica se o usuário já existe na lista
                bool usuarioJaExiste = OBJUsuarios.Any(u => u.UsuarioAcessoSAP == UsuarioAcessoSAP);

                if (!usuarioJaExiste)
                {
                    // Adiciona o usuário à lista
                    ComunicacaoEspecificaServiceLayerSAPClass novoUsuario = new ComunicacaoEspecificaServiceLayerSAPClass
                    {
                        UsuarioAcessoSAP = this.UsuarioAcessoSAP,
                        SenhaUsuarioAcessoSAP = this.SenhaUsuarioAcessoSAP
                    };

                    OBJUsuarios.Add(novoUsuario);
                }
            }
            catch (Exception ex)
            {
                erro = "Erro ao adicionar o usuário. Erro: " + ex.Message;
            }

            //Atribui Classe para Application
            if (erro == "")
            {
                HttpContext.Current.Application.Lock();
                HttpContext.Current.Application["ComunicacaoEspecificaListaServiceLayerSAPClass"] = this;
                HttpContext.Current.Application.UnLock();
            }

            return erro;
        }

        public void LimparDados()
        {
            foreach (ComunicacaoEspecificaServiceLayerSAPClass OBJUsuario  in OBJUsuarios)
            {
                OBJUsuario.JSONEnvio = string.Empty;
                OBJUsuario.JSONRetorno = string.Empty;
                OBJUsuario.CodigoClienteSAP = string.Empty;
                OBJUsuario.CodigoClienteTipoContato = string.Empty;
                OBJUsuario.CodigoClienteLinha = 0;
                OBJUsuario.CodigoClientePrimeiroNome = string.Empty;
                OBJUsuario.CodigoClienteUltimoNome = string.Empty;
                OBJUsuario.CodigoClienteEmail = string.Empty;
                OBJUsuario.CodigoClienteTelefone1 = string.Empty;
                OBJUsuario.AprovacaoNumero = 0;
                OBJUsuario.AprovacaoUsuario = string.Empty;
                OBJUsuario.AprovacaoUsuarioSenha = string.Empty;
                OBJUsuario.AprovacaoHistorico = string.Empty;
                OBJUsuario.AprovacaoDecisao = string.Empty;
                OBJUsuario.EsbocoChaveSAP = 0;
                OBJUsuario.EsbocoNovoPedidoSAP = string.Empty;
                OBJUsuario.DataLancamentoPedido = DateTime.MinValue;
                OBJUsuario.DataEntregaPedido = DateTime.MinValue;
                OBJUsuario.DataCancelamentoPedido = DateTime.MinValue;
                OBJUsuario.EsbocoNovaNotaSAP = string.Empty;
                OBJUsuario.NumeroPedidoSAP = 0;
                OBJUsuario.HistoricoPedidoSAP = string.Empty;
                OBJUsuario.HistoricoAnteriorPedidoSAP = string.Empty;
                OBJUsuario.NumeroPrimarioNotaSAP = 0;
                OBJUsuario.HistoricoNotaSAP = "";
                OBJUsuario.EsbocoChaveSAP= 0;
                OBJUsuario.EsbocoNovoPedidoSAP = string.Empty;
                OBJUsuario.DataLancamentoPedido = DateTime.MinValue;
	            OBJUsuario.DataEntregaPedido = DateTime.MinValue;
                OBJUsuario.DataCancelamentoPedido = DateTime.MinValue;
                OBJUsuario.EsbocoNovaNotaSAP = string.Empty;
                OBJUsuario.NumeroPedidoSAP = 0;
                OBJUsuario.HistoricoPedidoSAP = string.Empty;
            }
        }
    }
}
