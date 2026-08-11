<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="cabecarioLogistica.ascx.cs" Inherits="VendasWeb.usercontrol.cabecarioLogistica" %>

<!-- Cabecario Entidade -->
    <div id="entCabecario" class="detCabeccario">
        <asp:Label ID="lblEmpresa" runat="server" Text="EMPRESA:" CssClass="texto" ></asp:Label>
        <asp:Label ID="lblDescEmpresa"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Literal ID="ltlNumPedido" runat="server"></asp:Literal>
        <!--<asp:Label ID="lblNumPedido" runat="server" Text="Número:" CssClass="texto" ></asp:Label>
        <asp:Label ID="lblDescNumPedido"  runat="server" Text="" CssClass="texto"></asp:Label><br /> -->
        <asp:Label ID="lblnome" runat="server" Text="NOME:" CssClass="texto" ></asp:Label> 
        <asp:Label ID="lblDescNome"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Label ID="lblFantasia" runat="server" Text="FANTASIA:" CssClass="texto"></asp:Label>
        <asp:Label ID="lblDescFantasia"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:Label ID="lblCnpj" runat="server" Text="CNPJ/CPF:" CssClass="texto"></asp:Label>
        <asp:Label ID="lblDescCnpj"  runat="server" Text="" CssClass="texto"></asp:Label><br />
        <asp:TextBox ID="txtIDEntidade" runat="server" Visible="false"></asp:TextBox>
        <div id="btnentidade" class="btnDireita">
            <asp:Button ID="btnAlteraEntidade" runat="server" Text="Voltar Principal" 
                CssClass="Botoes" onclick="btnAlteraEntidade_Click" />
        </div>
    </div>
