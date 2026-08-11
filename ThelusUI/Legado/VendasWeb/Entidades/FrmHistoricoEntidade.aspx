<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmHistoricoEntidade.aspx.cs" Inherits="VendasWeb.WebVendas.Entidade.FrmHistoricoEntidade" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="../../css/listas.css?aux=6" />
     <style type="text/css">
         .Botoes
         {}
     </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
<div>
    
    <b><center>Histórico da Entidade</center></b>
        
   
        <br />
        <div style=" width:430px; float:left; position:relative; " >
          <table class="lstTabela"><tr class="tabLstCab"><td colspan="5" align="center">
            Histórico Anterior:</td></tr>
            <!-- <td align="center"><a href="#" class="imgeditent"><img src="../imagens/adiciona.png" alt="Alteração" border="0" /></a></td></tr> -->
            <tr><td colspan="5">
                <asp:TextBox ID="txtEntTextoHist" runat="server" class="campo" 
                    TextMode="MultiLine" Width="406px" Height="347px" 
                    ></asp:TextBox></td></tr>
        </table>
        </div>



        <!-- Dados Observacao -->
        &nbsp;&nbsp;&nbsp;&nbsp;
        <div style=" width:450px; float:left; position:relative; "  >
            <!-- Dados Histrico -->
            <table class="lstTabela"><tr class="tabLstCab"><td colspan="5" align="center">Novo Histórico Entidade</td></tr>
                
                <tr><td colspan="5">
                    <asp:TextBox ID="txtNovoHistorico" runat="server" class="campo" 
                         TextMode="MultiLine" Width="406px" Height="347px"></asp:TextBox></td></tr>
            </table>
            
            
            <br /> 
            
             <asp:LinkButton ID="AdcionarButton" class="btn btn-success" runat="server" CausesValidation="False" Visible="false"
            OnClick="btnSalvar_Click" title="Salvar Dados" data-rel="tooltip">
                                                    <span class="glyphicon glyphicons-ok" aria-hidden="true"> Salvar Dados</span> 

        </asp:LinkButton>



            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            
            <asp:LinkButton ID="CancelarOperacaoLinkButton" class="btn btn-danger" runat="server"
                OnClick="CancelarButton_Click" title="Retornar a Lista de Entidade" CausesValidation="False" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Retornar</span> 

            </asp:LinkButton>
			
			


        </div>

       
           
</div>           
        

   
   

</asp:Content>
