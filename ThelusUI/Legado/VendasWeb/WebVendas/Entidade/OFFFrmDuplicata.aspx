<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="OFFFrmDuplicata.aspx.cs" Inherits="VendasWeb.WebVendas.Entidade.FrmDuplicata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" type="text/css" href="../../css/listas.css?aux=6" />
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   
<div>
   
     <div id="dadComplementares" >        
        <div id="dadHist" class="dadCompleDup">
            <!-- Dados Histrico -->
            <table class="lstTabela"><tr class="tabLstCab"><td colspan="50" align="center">Histórico Financeiro</td></tr>
                
                <tr ><td style=" width:848px; height:300px;" colspan="5">
               
                <div  class="CampoOverflow">
                <br />
                <asp:Label ID="LblEntidade" runat="server" Text=""></asp:Label><br /><br />
               <center> <asp:Label ID="lblFinanceiro" runat="server" Text=""></asp:Label></center>
                </div>
               </td></tr>
                
            </table>

            <asp:Button ID="VoltarButton"  CssClass="Botoes" runat="server" Text="Voltar" 
                onclick="VoltarButton_Click" />
        </div>

       
    </div>
        

</div>


</asp:Content>
