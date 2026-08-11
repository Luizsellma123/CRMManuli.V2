<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="frmCadEntidade.aspx.cs" Inherits="VendasWeb.cadastros.frmCadEntidade" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="../css/listas.css?aux=5" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Menu ID="tabMenu" runat="server" Orientation="Horizontal" OnMenuItemClick="tabMenu_MenuItemClick">
        <Items>
            <asp:MenuItem Text="Principal" Value="t1" />
            <asp:MenuItem Text="Gerencial" Value="t2" />
            <asp:MenuItem Text="Complementar" Value="t3" />
        </Items>

        <StaticMenuStyle CssClass="tabCadCli" />
        <StaticMenuItemStyle CssClass="itemCadCli" />
        <StaticSelectedStyle CssClass="selectedTabCadCli" />
    </asp:Menu>
       
    <div class="conteudoCadCli">
        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewPrincipal" runat="server"><br />
                <asp:Label ID="lblEntCod" runat="server" Text="Código:" Width="90px" CssClass="texto" Visible="true"></asp:Label>
                <asp:TextBox ID="txtEntCod" runat="server" Width="120px" Visible="true"></asp:TextBox><br /><br />
                
                <asp:Label ID="lblEntNome"  runat="server" Text="Nome:" Width="90px" CssClass="texto"></asp:Label>
                <!--<asp:DropDownList ID="drpTratamento" runat="server" Width="60" Visible="false"></asp:DropDownList>--> 
                <asp:TextBox ID="txtEntNome" runat="server" Width="670px" Height="22px"></asp:TextBox><br /> <br />
                <asp:Label ID="lblEntFantasia" runat="server" Text="Fantasia:" Width="90px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtEntFantasia" runat="server" Width="670"></asp:TextBox><br /><br />

                <asp:Label ID="lblCep" runat="server" Text="CEP:" Width="90px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtCep" runat="server" Width="120px"></asp:TextBox>

                <asp:Label ID="Label1" runat="server" Text=" " Width="20px"></asp:Label>                
                <asp:Label ID="lblEndereco" runat="server" Text="Endereço:" Width="70px" CssClass="texto"></asp:Label>
                <!--<asp:DropDownList ID="drpTipoLograd" runat="server" Width="60"></asp:DropDownList>-->
                <asp:TextBox ID="txtRua" runat="server" Width="160px"></asp:TextBox>
                <asp:Label ID="Label2" runat="server" Text=" " Width="20px"></asp:Label>                
                <asp:Label ID="lblNumero" runat="server" Text="Número:" Width="60px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtNumero" runat="server" Width="170px"></asp:TextBox>
                <br /><br />

                <asp:Label ID="lblComplemento" runat="server" Text="Complemento:" Width="90px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtComplemento" runat="server" Width="120px"></asp:TextBox>
                <asp:Label ID="Label3" runat="server" Text=" " Width="20px"></asp:Label>                
                <asp:Label ID="lblBairro" runat="server" Text="Bairro:" Width="70px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtBairro" runat="server" Width="160px"></asp:TextBox>
                <asp:Label ID="Label7" runat="server" Text=" " Width="20px"></asp:Label>
                <asp:Label ID="lblCidade" runat="server" Text="Cidade:" Width="60px" CssClass="texto"></asp:Label>
                <asp:DropDownList ID="drpCidade" runat="server" CssClass="campo" Width="170px" AutoPostBack="True"></asp:DropDownList>
                <br /><br />                
                <asp:Label ID="lblCaixaPostal" runat="server" Text="Cx. Postal:" Width="90px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtCaixaPostal" runat="server" Width="120px"></asp:TextBox>
                <asp:Label ID="Label4" runat="server" Text=" " Width="20px"></asp:Label>                
                <asp:Label ID="lblTipoInsc" runat="server" Text="Tipo Insc:" Width="70px" CssClass="texto"></asp:Label>
                <asp:DropDownList ID="drpTipoInsc" runat="server" CssClass="campo" Width="160px">
                <asp:ListItem Value="Jurídica" Selected="True">Jurídica</asp:ListItem>
                <asp:ListItem Value="Física">Física</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label6" runat="server" Text=" " Width="20px"></asp:Label>                
                <asp:Label ID="lblCNPJ" runat="server" Text="CNPJ:" Width="60px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtCNPJ" runat="server" ontextchanged="txtCNPJ_TextChanged" Width="170px"></asp:TextBox>     
                <br /><br />                
                <asp:Label ID="lblInscricaoEstadual" runat="server" Text="Insc. Estadual:" Width="90px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtInscricaoEstadual" runat="server" Width="120px"></asp:TextBox>                        
            </asp:View>

            <asp:View ID="ViewGerencial" runat="server"><br />
                <asp:Label ID="lblEmail" runat="server" Text="E-mail:" Width="50px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" Width="400px"></asp:TextBox><br /><br />

                <asp:Label ID="lblSite" runat="server" Text="Site:" Width="50px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtSite" runat="server" Width="400px"></asp:TextBox><br /><br />

                <asp:Label ID="lblDDD" runat="server" Text="DDD:" Width="50px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtDDD" runat="server" Width="150px"></asp:TextBox><br /><br />

                <asp:Label ID="lblFone" runat="server" Text="Fone:" Width="50px" CssClass="texto"></asp:Label>
                <asp:TextBox ID="txtFone" runat="server" Width="150px"></asp:TextBox><br /><br />

                <!--<br /><br />
                <asp:Label ID="lblTipoCobranca" runat="server" Text="Tipo Cobrança:" Width="100px" CssClass="texto" ></asp:Label> 
                <asp:TextBox ID="txtTipoCobranca" runat="server" Width="150"></asp:TextBox><br /><br />-->
            </asp:View>

            <asp:View ID="ViewComplementar" runat="server"><br />
                <!--<asp:Label ID="lblRegiao" runat="server" Text="Região:" Width="70px" CssClass="texto" ></asp:Label> 
                <asp:TextBox ID="txtRegiao" runat="server" Width="150px"></asp:TextBox><br /><br />-->

                <asp:Label ID="lblNatureza" runat="server" Text="Natureza:" Width="70px" CssClass="texto" ></asp:Label> 
                <asp:DropDownList ID="drpNatureza" runat="server" CssClass="campo" Width="400">
                    <asp:ListItem Value="Atacadista">Atacadista</asp:ListItem>
                    <asp:ListItem Value="Consórcio">Consórcio</asp:ListItem>
                    <asp:ListItem Value="Contrutora">Contrutora</asp:ListItem>
                    <asp:ListItem Value="Consumidor">Consumidor</asp:ListItem>
                    <asp:ListItem Value="Consumidor Contribuinte">Consumidor Contribuinte</asp:ListItem>
                    <asp:ListItem Value="Distribuidor">Distribuidor</asp:ListItem>
                    <asp:ListItem Value="Entidade Governamental">Entidade Governamental</asp:ListItem>
                    <asp:ListItem Value="Exportador">Exportador</asp:ListItem>
                    <asp:ListItem Value="Fabricante" Selected="True">Fabricante</asp:ListItem>            
                    <asp:ListItem Value="Importador">Importador</asp:ListItem>
                    <asp:ListItem Value="Prestador de Serviços">Prestador de Serviços</asp:ListItem>
                    <asp:ListItem Value="Produtor Rural">Produtor Rural</asp:ListItem>
                    <asp:ListItem Value="Representante">Representante</asp:ListItem>
                    <asp:ListItem Value="Revendedor">Revendedor</asp:ListItem>
                    <asp:ListItem Value="Varejista">Varejista</asp:ListItem>
                    <asp:ListItem Value="Outros">Outros</asp:ListItem>
                    <asp:ListItem Value="Transportador">Transportador</asp:ListItem>         
                    <asp:ListItem Value="Motorista">Motorista</asp:ListItem>
                </asp:DropDownList><br /><br />

                <asp:Label ID="lblStatus" runat="server" Text="Status:" Width="70px" CssClass="texto"></asp:Label>
                <asp:DropDownList ID="drpStatus" runat="server" Width="400"></asp:DropDownList><br /><br />

                <asp:Label ID="lblVendedor" runat="server" Text="Vendedor:" Width="70px" CssClass="texto"></asp:Label>
                <asp:DropDownList ID="drpVendedor" runat="server" CssClass="campo" Width="400px"></asp:DropDownList><br /><br />

                <asp:Label ID="lblCategoria" runat="server" Text="Categoria:" Width="70px" CssClass="texto"></asp:Label>
                <asp:DropDownList ID="drpCategoria" runat="server" CssClass="campo" Width="400px"></asp:DropDownList>
                <!--<asp:Label ID="lblOrigem" runat="server" Text="Origem:" Width="70px" CssClass="texto" ></asp:Label> 
                <asp:TextBox ID="txtOrigem" runat="server" Width="150px"></asp:TextBox><br /><br />-->
            </asp:View>            
        </asp:MultiView>        
    </div> 
    <asp:Button ID="SalvarButton" runat="server" Text="Salvar" CssClass="Botoes" onclick="SalvarButton_Click" />
    <asp:Button ID="RetornarButton" runat="server" Text="Retornar" CssClass="Botoes" onclick="RetornarButton_Click" />
    <asp:Button ID="EnviarAnaliseButton" runat="server" Text="Enviar análise" CssClass="Botoes" onclick="EnviarAnaliseButton_Click" />

</asp:Content>
