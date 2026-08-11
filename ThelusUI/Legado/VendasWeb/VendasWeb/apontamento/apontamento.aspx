<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="apontamento.aspx.cs" Inherits="VendasWeb.apontamento.apontamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=6" />   
    <script language="javascript" src="../Scripts/jquery1.4.1.js" type="text/javascript"></script>
     <script language="javascript" src="../Scripts/jquery.maskedinput.js" type="text/javascript"></script>
     <script language="javascript" src="../js/jsApontamento.js" type="text/javascript"></script>

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<br />
<center><p><b><asp:Label ID="Label1" runat="server" Text="Apontamento da Produção" Width="250px" CssClass="textoTitulo"></asp:Label></b></p></center>
<br />
<div >
   <asp:Label ID="lblEmpresa" runat="server" Text="Empresa: " Width="145px" CssClass="texto"></asp:Label>
   <asp:DropDownList ID="drpEmpresa" runat="server" Width="145px" CssClass="campo" >  </asp:DropDownList>
    
    <br /><br />
    <asp:Label ID="lblOrdemOp" runat="server" Text="Ordem de Produção: " Width="145px" CssClass="texto"></asp:Label>
    <asp:TextBox ID="txtOrdemProducao" runat="server" Width="145px" 
        CssClass="campo" AutoPostBack="True" 
        ontextchanged="txtOrdemProducao_TextChanged" ></asp:TextBox>

    <asp:Label ID="lblTipoOperacao" runat="server" Text="Tipo Operação: " CssClass="CampoMeio" Width="145px"></asp:Label>
    <asp:DropDownList ID="drpTipoOperacao" runat="server" Width="145px" CssClass="campo" > 
    <asp:ListItem >Operação</asp:ListItem>
    <asp:ListItem >Preparação</asp:ListItem>
    <asp:ListItem >Transporte</asp:ListItem>
    <asp:ListItem >Conferência</asp:ListItem>
    </asp:DropDownList>

    <br /><br />
    <asp:Label ID="lblPlanejamento" runat="server" Text="Planejamento: " Width="145px" CssClass="texto" ></asp:Label>
    <asp:TextBox ID="txtPlanejamento" runat="server" ReadOnly="true" Width="145px" CssClass="campo"></asp:TextBox>

    <asp:Label ID="lblProduto" runat="server" Text="Produto: " CssClass="CampoMeio" Width="145px"></asp:Label>
    <asp:TextBox ID="txtProduto" runat="server" ReadOnly="true" Width="145px" CssClass="campo"></asp:TextBox>
    <asp:Label ID="lblQuantidadeNecessaria" runat="server" Text="Qtd. Necessaria: " CssClass="CampoMeio" Width="100px"></asp:Label>
    <asp:TextBox ID="txtQtdNecessaria" runat="server" ReadOnly="true" Width="145px" CssClass="campo"></asp:TextBox>

    <br /><br />
    <asp:Label ID="lblSequencia" runat="server" Text="Sequência: " Width="145px" CssClass="texto"></asp:Label>
    <asp:TextBox ID="txtSequencia" runat="server" ReadOnly="true" Width="145px" CssClass="campo"></asp:TextBox>
    

     <asp:Label ID="lblOperacao" runat="server" Text="Operação: "  CssClass="CampoMeio" Width="145px"   > </asp:Label>
     <asp:TextBox ID="txtOperacao" runat="server" ReadOnly="true" Width="145px" CssClass="campo"></asp:TextBox>

     <asp:Label ID="lblStatus" runat="server" Text="Status: " CssClass="CampoMeio" Width="100px"></asp:Label>
     <asp:DropDownList ID="drpStatus" runat="server" Width="145px" CssClass="campo">
     <asp:ListItem >Manual</asp:ListItem>
     </asp:DropDownList>



     <br /><br />
     <asp:Label ID="lblAtividade" runat="server" Text="Atividade: " Width="145px" CssClass="texto"></asp:Label>
     <asp:TextBox ID="txtAtividade" runat="server" Width="145px" CssClass="campo" ReadOnly="true" ></asp:TextBox>
     <asp:Label ID="lblAtividadeText" runat="server" Text="" Width="400px" CssClass="texto" ></asp:Label>

     <br /><br />
     <asp:Label ID="lblCentroControle" runat="server" Text="Centro de Controle: " CssClass="texto" Width="145px"></asp:Label>
     <asp:TextBox ID="txtCentroControle" runat="server" Width="145px" CssClass="campo" ReadOnly="true" ></asp:TextBox>
     <asp:Label ID="lblCentroControleText" runat="server" Text="" CssClass="texto" Width="400px"></asp:Label>

     <br /><br />
     <asp:Label ID="lblDataInicial" runat="server" Text="Data Inicial: " Width="145px" CssClass="texto"></asp:Label>
     <asp:TextBox ID="txtDataInicial" runat="server" Width="145px" CssClass="campo"></asp:TextBox>

     <asp:Label ID="lblHoraInicial" runat="server" Text="Hora: " CssClass="CampoMeio" Width="145px"></asp:Label>
     <asp:TextBox ID="txtHoraInicial" runat="server" Width="145px" CssClass="campo"></asp:TextBox>

     <br /><br />
     <asp:Label ID="lblDataFinal" runat="server" Text="Data Final: " Width="145px" CssClass="texto"></asp:Label>
     <asp:TextBox ID="txtDataFinal" runat="server" Width="145px" CssClass="campo"></asp:TextBox>


     <asp:Label ID="lblHoraFinal" runat="server" Text="Hora: " CssClass="CampoMeio" Width="145px"></asp:Label>
     <asp:TextBox ID="txtHoraFinal" runat="server" Width="145px" CssClass="campo"></asp:TextBox>


     <br /><br />
     <asp:Label ID="lblBoa" runat="server" Text="Boa: " Width="145px" CssClass="texto"></asp:Label>
     <asp:TextBox ID="txtQtdBoa" runat="server" Width="145px" CssClass="campo" Text = "0"></asp:TextBox>

     <asp:Label ID="lblRefugada" runat="server" Text="Refugada: " CssClass="CampoMeio" Width="145px"></asp:Label>
     <asp:TextBox ID="txtQtdRefugada" runat="server" Width="145px" CssClass="campo" Text = "0"></asp:TextBox>

     <br /><br />
     <asp:Label ID="lblReprocesso" runat="server" Text="Reprocesso: " Width="145px" CssClass="texto"></asp:Label>
     <asp:TextBox ID="txtQtdReprocesso" runat="server" Width="145px" CssClass="campo" Text = "0"></asp:TextBox>

     <asp:Label ID="lblRetalho" runat="server" Text="Retalho: " CssClass="CampoMeio" Width="145px"></asp:Label>
     <asp:TextBox ID="txtQtdRetalho" runat="server" Width="145px" CssClass="campo" Text = "0"> </asp:TextBox>


      <!--Lista de Funcionarios -->
     <br /><br />
        
    <div id="lstItem" class="detCorpo">
        <table class="lstTabela">
            <tr class="tabLstCab">
                <td align="center">
                    <asp:Button ID="btnIncluir" runat="server" Text="" CssClass="btAdiciona" 
                        onclick="btnIncluir_Click" Width="26px" /></td>
                
                <td class="small" style="width: 153px"><asp:Label ID="lblUnidade" runat="server" Text="Codigo:"></asp:Label></td>
                <td class="extendproduto" style="width: 808px"><asp:Label ID="Label2" runat="server" Text="Nome:"></asp:Label></td>
                
                
            </tr>
            
            <!-- Items carregados dinamicamente -->
            <asp:Literal ID="ltlItems" runat="server"></asp:Literal>

        </table>
        
    </div>


     <br /><br /><br />
     <div>
    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" CssClass="Botoes" 
             Width="100px" onclick="btnSalvar_Click" />
     
    &nbsp;<asp:Button ID="tbnCancelar" runat="server" Text="Cancelar" CssClass="Botoes" 
             Width="100px" onclick="tbnCancelar_Click"/>
    </div>
</div>


<asp:HiddenField ID="FuncCodDelet" runat="server" value=""/>
</asp:Content>
