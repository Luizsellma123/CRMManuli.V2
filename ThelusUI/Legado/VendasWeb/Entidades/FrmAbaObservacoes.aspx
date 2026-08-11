<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmAbaObservacoes.aspx.cs" Inherits="VendasWeb.Entidades.FrmAbaObservacoes" %>
<%@ Register src="../usercontrol/ControlEntidade.ascx" tagname="ControlEntidade" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />
    <link rel="stylesheet" type="text/css" href="../css/ListaPedido.css?aux=6" />
    <script language="javascript"  src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


<div class="conteudo">

         <center><b><h3>Cadastro de Cliente - Histórico</h3></b></center>
        <uc1:ControlEntidade ID="ControlEntidade" runat="server" />
        <br />

    <asp:Label ID="ObservacaoAnteriorLabel" runat="server" Text="Informações Adicionais Anteriores:" Visible="false"></asp:Label><br />
    <asp:TextBox ID="ObservacaoAnteriorTextBox" runat="server" Height="136px" TextMode="MultiLine" Width="785px" Visible="false"></asp:TextBox>    

    <br /><br />
     <asp:Label ID="ObservacaoLabel" runat="server" Text="Adicione alguma Informação Adicional:"></asp:Label><br />
      <asp:TextBox ID="ObservacaoTextBox" runat="server" Height="136px" TextMode="MultiLine" Width="785px"></asp:TextBox>    

        <br /><br />

            <div>
    

                       <asp:LinkButton ID="FinalizarButton" class="btn btn-success" runat="server" 
                      OnClick="FinalizarButton_Click" title="Finalizar Cadastro"  data-rel="tooltip" >
                            <span class="glyphicon glyphicons-circle-ok" aria-hidden="true"> Finalizar Cadastro</span> 

                        </asp:LinkButton>


                    &nbsp;<asp:LinkButton ID="AlterarButton" class="btn btn-warning" runat="server" Visible="false"
                      OnClick="AlterarButton_Click" title="Histórico"  data-rel="tooltip" >
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> Incluir Informação Adicional</span> 

                        </asp:LinkButton>
                     
              
    &nbsp;<asp:LinkButton ID="PrincipalButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
                      OnClick="PrincipalButton_Click" title="Principal"  data-rel="tooltip" >
                            <span class="glyphicon glyphicon-compressed" aria-hidden="true"> Principal </span> 

     </asp:LinkButton>
    

    &nbsp;<asp:LinkButton ID="ContatoButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
                      OnClick="ContatoButton_Click" title="Contato"  data-rel="tooltip" >
                            <span class="glyphicon glyphicon-user" aria-hidden="true"> Contato</span> 

     </asp:LinkButton>

    &nbsp;<asp:LinkButton ID="EnderecoEntregaButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
                      OnClick="EnderecoEntregaButton_Click" title="Endereços de Entrega"  data-rel="tooltip" >
                            <span class="glyphicon glyphicon-list" aria-hidden="true"> End. Entrega</span> 

     </asp:LinkButton>
    
    
        &nbsp;<asp:LinkButton ID="FiscalLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
                      title="Fiscal"  data-rel="tooltip" OnClick="FiscalLinkButton_Click" >
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Fiscal</span> 

     </asp:LinkButton>



    &nbsp;<asp:LinkButton ID="PedidosLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="PedidosButton_Click" title="Pedidos" data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> Pedidos</span> 

    </asp:LinkButton>



    &nbsp;
    
    </div>

<br />  
<div>

    <asp:LinkButton ID="InformacoesButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
                      OnClick="InformacoesButton_Click" title="Informações"  data-rel="tooltip" >
                            <span class="glyphicon glyphicon-folder-open" aria-hidden="true"> Informações</span> 

     </asp:LinkButton>
    

    &nbsp;<asp:LinkButton ID="AnexosButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
                      OnClick="AnexosButton_Click" title="Anexos"  data-rel="tooltip" >
                            <span class="glyphicon glyphicon-paperclip" aria-hidden="true"> Anexos</span> 

     </asp:LinkButton>
    

&nbsp;<asp:LinkButton ID="HoldingLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="HoldingButton_Click" title="Observações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Holding</span> 

    </asp:LinkButton>
    
                 
&nbsp;<asp:LinkButton ID="LogisticaLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="LogisticaButton_Click" title="Observações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-transfer" aria-hidden="true"> Logistica</span> 
        
    </asp:LinkButton>

                 &nbsp;<asp:LinkButton ID="VendedorLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="VendedorButton_Click" title="Vendedor" data-rel="tooltip">
                            <span class="glyphicon glyphicon-user" aria-hidden="true"> Vendedor</span> 
        
    </asp:LinkButton>

         &nbsp;<asp:LinkButton ID="DuplicataLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="DuplicatasButton_Click" title="Duplicatas" data-rel="tooltip">
                            <span class="glyphicon glyphicon-paperclip" aria-hidden="true"> Duplicatas</span> 
        
    </asp:LinkButton>

                &nbsp;<asp:LinkButton ID="NotasLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="NotasButton_Click" title="Notas" data-rel="tooltip">
                            <span class="glyphicon glyphicon-book" aria-hidden="true"> Notas</span> 
        
    </asp:LinkButton>


        &nbsp;<asp:LinkButton ID="AgendaLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="AgendaButton_Click" title="Agenda" data-rel="tooltip">
                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"> Agenda</span> 
        
    </asp:LinkButton>



                &nbsp;<asp:LinkButton ID="CRMLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="CrmButton_Click" title="CRM" data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> CRM</span> 
        
    </asp:LinkButton>


</div>

<br />

        <div>

           <asp:LinkButton ID="CancelarOperacaoLinkButton" class="btn btn-danger" runat="server" Visible="false"
                OnClick="CancelarOperacaoButton_Click" title="Retornar a Lista de Entidade" CausesValidation="False" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Retornar</span> 

            </asp:LinkButton>
			
			

        </div>
          <br />


   </div>


</asp:Content>
