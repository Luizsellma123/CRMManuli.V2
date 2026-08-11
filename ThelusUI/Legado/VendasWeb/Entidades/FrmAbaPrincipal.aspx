<%@ Page Title="" Language="C#" MasterPageFile="~/VendasWeb.Master" AutoEventWireup="true" CodeBehind="FrmAbaPrincipal.aspx.cs" Inherits="VendasWeb.Entidades.FrmAbaPrincipal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>" type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>" type="text/javascript"></script>

    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


   <div class="conteudo">

        <center><b><h3>Cadastro de Cliente</h3></b></center>
        <br />


        <div class="Principal">

           <div  style="float:left">
             <asp:Label ID="Label19" runat="server" Text="">Logado:</asp:Label>
             <asp:Label ID="VendedorLogadoLabel" runat="server" Text=""></asp:Label>
             
           </div>
            
            <div style="float:right">
                <asp:Label  ID="DataCadastroLabel" Text="" runat="server" ></asp:Label>
                <br />
                 <asp:Label  ID="StatusLabel1" Text="Status de Cadastro:" runat="server" ></asp:Label><asp:Label  ID="StatusLabel" Text="" runat="server" ></asp:Label>
            </div>

            <br />
            
            <!--Filtro Vendedor-->
            <asp:Label ID="VendCodLabel" runat="server" Text="">Escolher Vendedor:</asp:Label>
            <asp:DropDownList ID="VendCodDropDownList" runat="server">
            </asp:DropDownList>


            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic" SetFocusOnError="True"
                ControlToValidate="VendCodDropDownList" ErrorMessage="Selecione um Vendedor!"></asp:RequiredFieldValidator>


            
                            
            <br />
            <br />



            <div class="conteudo">
                <div class="Principal">
                    <asp:Label ID="Label3" runat="server" Text="CNPJ/CPF:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="Cnpj_CpfTextBox" runat="server" AutoPostBack="true" 
                        onkeypress="mascara( this, mnum );" OnTextChanged="Cnpj_CpfTextBox_TextChanged" 
                        Width="151px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="Cnpj_CpfTextBox" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="razaoSocialLabel" runat="server" Text="Razão Social:"></asp:Label>
                    &nbsp;&nbsp;
                    <asp:TextBox ID="razaoSocialTextBox" runat="server" CssClass="uppercase" 
                        MaxLength="100" Width="730px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="razaoSocialTextBox" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Nome Fantasia:"></asp:Label>
                    <asp:TextBox ID="NomeFantasiaTextBox" runat="server" CssClass="uppercase" 
                        MaxLength="40" Width="375px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="NomeFantasiaTextBox" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;
                    <asp:Label ID="Label2" runat="server" Text="Email XML:"></asp:Label>
                    <asp:TextBox ID="EmailXmlTextBox" runat="server" CssClass="lowercase" 
                         Width="270px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="EmailXmlTextBox" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="EmailRegularExpressionValidator" 
                        runat="server" ControlToValidate="EmailXmlTextBox" Display="Dynamic" 
                        ErrorMessage="Email Invalido" ForeColor="Red" SetFocusOnError="True" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email Inválido</asp:RegularExpressionValidator>
                    <br />
                    <br />
                    <asp:Label ID="Label5" runat="server" Text="CEP:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="CepTextBox" runat="server" AutoPostBack="true" 
                        CausesValidation="False" onkeypress="mascara( this, mcep );" 
                        OnTextChanged="CepTextBox_TextChanged" Width="151px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                        ControlToValidate="CepTextBox" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="Label6" runat="server" Text="Endereço:"></asp:Label>
                    <asp:TextBox ID="EnderecoTextBox" runat="server" CssClass="uppercase" 
                         Width="380px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" 
                        ControlToValidate="EnderecoTextBox" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="Label18" runat="server" Text="Número:"></asp:Label>
                    <asp:TextBox ID="NumeroTextBox" runat="server" 
                        OnTextChanged="NumeroTextBox_TextChanged" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" 
                        ControlToValidate="NumeroTextBox" Display="Dynamic" 
                        ErrorMessage="Preencher S/N para sem Número" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="NumeroTextBox" Display="Dynamic" 
                        ErrorMessage="Preencher S/N para sem Número" ForeColor="Red" 
                        SetFocusOnError="True" ValidationExpression="((\d+$)|([sS]+[/]+[nN]))$">Preencher S/N para sem Número</asp:RegularExpressionValidator>
                    <br />
                    <br />
                    <asp:Label ID="Label7" runat="server" Text="Bairro:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="BairroTextBox" runat="server" CssClass="uppercase" 
                         Width="151px" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" 
                        ControlToValidate="BairroTextBox" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="Label9" runat="server" Text="UF:"></asp:Label>
                    <asp:TextBox ID="UFTextBox" runat="server" CssClass="uppercase" 
                        MaxLength="2" OnTextChanged="UFTextBox_TextChanged" Width="30px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" 
                        ControlToValidate="UFTextBox" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="Label8" runat="server" Text="Cidade:"></asp:Label>
                    <asp:DropDownList ID="CidadeDropDownList" runat="server" AutoPostBack="True" 
                        OnSelectedIndexChanged="CidadeDropDownList_SelectedIndexChanged" Width="151px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" 
                        ControlToValidate="CidadeDropDownList" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="Label10" runat="server" Text="Complemento:"></asp:Label>
                    <asp:TextBox ID="ComplementoTextBox" runat="server" CssClass="uppercase" 
                        Width="192px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label4" runat="server" Text="Inscrição Estadual:"></asp:Label>
                    <asp:TextBox ID="InscricaoEstadualTextBox" runat="server" AutoPostBack="True" 
                        CssClass="uppercase" OnTextChanged="InscricaoEstadualTextBox_TextChanged" 
                        Width="180px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                        ControlToValidate="InscricaoEstadualTextBox" Display="Dynamic" 
                        ErrorMessage="Preencher ISENTO" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="SuframaLabel" runat="server" Text="Suframa Nº:" Visible="false"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="SuframaTextBox" runat="server" 
                        onkeypress="mascara( this, mnum );" Visible="false" Width="151px"></asp:TextBox>
                    &nbsp;<asp:Label ID="Label16" runat="server" Text="Concessão:" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ConcessaoDropDownList" runat="server" Visible="false" 
                        Width="151px">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Label ID="Label11" runat="server" Text="Contato na Empresa:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="NomeResponsavelTextBox" runat="server" CssClass="uppercase" 
                        Width="296px"></asp:TextBox>
                    
                    <br />
                    <br />
                    <asp:Label ID="Label17" runat="server" Text="Email:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="EmailTextBox" runat="server" CssClass="lowercase" 
                         Width="420px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" 
                        ControlToValidate="EmailTextBox" Display="Dynamic" ErrorMessage="*" 
                        SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="EmailTextBox" Display="Dynamic" 
                        ErrorMessage="Email Invalido" ForeColor="Red" SetFocusOnError="True" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Email Inválido</asp:RegularExpressionValidator>
                    <br />
                    <br />
                    <asp:Label ID="Label12" runat="server" Text="Telefone:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="DDDTelefoneResponsavelTextBox" runat="server" MaxLength="2" 
                        onkeypress="mascara( this, mnum );" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" 
                        ControlToValidate="DDDTelefoneResponsavelTextBox" Display="Dynamic" 
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <asp:TextBox ID="TelefoneResponsavelTextBox" runat="server" 
                        onkeypress="mascara( this, mnum );" Width="151px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" 
                        ControlToValidate="TelefoneResponsavelTextBox" Display="Dynamic" 
                        ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    &nbsp;<asp:Label ID="Label13" runat="server" Text="Ramal:"></asp:Label>
                    <asp:TextBox ID="RamalTelefoneResponsavelTextBox" runat="server" Width="155px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label14" runat="server" Text="Celular:"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:TextBox ID="DDDCelularTextBox" runat="server" MaxLength="2" 
                        onkeypress="mascara( this, mnum );" Width="50px"></asp:TextBox>
                    <asp:TextBox ID="TelCelularTextBox" runat="server" 
                        onkeypress="mascara( this, mnum );" Width="151px"></asp:TextBox>
                    &nbsp;<asp:Label ID="CargoLabel" runat="server" Text="Cargo:"></asp:Label>
                    <asp:TextBox ID="CargoTextBox" runat="server" CssClass="uppercase"></asp:TextBox>
                </div>
            </div>




        </div>

    </div>
    <br />



    <div>


        <asp:LinkButton ID="ProximoButton" class="btn btn-primary" runat="server" CausesValidation="False"
                         OnClick="Passo1Button_Click" title="Próximo Passo" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-right" aria-hidden="true"> Próximo Passo</span> 

                     </asp:LinkButton>


        &nbsp;<asp:LinkButton ID="AlterarButton" class="btn btn-warning" runat="server" Visible="false"
                         OnClick="AlterarButton_Click" title="Alterar" data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> Alterar</span> 

                     </asp:LinkButton>


        &nbsp;<asp:LinkButton ID="ContatoButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="ContatoButton_Click" title="Contato" data-rel="tooltip">
                            <span class="glyphicon glyphicon-user" aria-hidden="true"> Contato</span> 

    </asp:LinkButton>

        &nbsp;<asp:LinkButton ID="EnderecoEntregaButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="EnderecoEntregaButton_Click" title="Endereços de Entrega" data-rel="tooltip">
                            <span class="glyphicon glyphicon-list" aria-hidden="true"> End. Entrega</span> 

    </asp:LinkButton>



        &nbsp;<asp:LinkButton ID="FiscalLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        title="Fiscal" data-rel="tooltip" OnClick="FiscalLinkButton_Click">
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Fiscal</span> 

    </asp:LinkButton>



        &nbsp;<asp:LinkButton ID="InformacoesButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="InformacoesButton_Click" title="Informações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-folder-open" aria-hidden="true"> Informações</span> 

    </asp:LinkButton>

     &nbsp;<asp:LinkButton ID="PedidosLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="PedidosButton_Click" title="Pedidos" data-rel="tooltip">
                            <span class="glyphicon glyphicon-edit" aria-hidden="true"> Pedidos</span> 

    </asp:LinkButton>


        &nbsp;
    



    </div>

    <br />
    <div>
    
    <asp:LinkButton ID="AnexosButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="AnexosButton_Click" title="Anexos" data-rel="tooltip">
                            <span class="glyphicon glyphicon-paperclip" aria-hidden="true"> Anexos</span> 

    </asp:LinkButton>


        &nbsp;<asp:LinkButton ID="ObservacoesButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="ObservacoesButton_Click" title="Observações" data-rel="tooltip">
                            <span class="glyphicon glyphicon-calendar" aria-hidden="true"> Observações</span> 

    </asp:LinkButton>



        &nbsp;<asp:LinkButton ID="HoldingLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="HoldingButton_Click" title="Holding" data-rel="tooltip">
                            <span class="glyphicon glyphicon-stats" aria-hidden="true"> Holding</span> 

    </asp:LinkButton>

                &nbsp;<asp:LinkButton ID="LogisticaLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        OnClick="LogisticaButton_Click" title="Logistica" data-rel="tooltip">
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

               <asp:LinkButton ID="CancelarOperacaoLinkButton" class="btn btn-danger" runat="server"
                OnClick="CancelarOperacaoButton_Click" title="Retornar a Lista de Entidade" CausesValidation="False" data-rel="tooltip">
                            <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Retornar</span> 

            </asp:LinkButton>
			


    </div>

    <br />

    <div>
    <asp:LinkButton ID="EnviarParaFiscalButton" class="btn btn-warning" runat="server" Visible="false" CausesValidation="False"
        OnClick="EnviarParaFicalButton_Click" title="Enviar para Analise Fiscal" data-rel="tooltip">
                            <span class="glyphicon glyphicon-new-window" aria-hidden="true"> Enviar para Analise Fiscal</span> 

    </asp:LinkButton>


    &nbsp;<asp:LinkButton ID="EnviarParaLogisticaButton" class="btn btn-warning" runat="server" Visible="false" CausesValidation="False"
        OnClick="EnviarParaLogisticaButton_Click" title="Enviar para Logistica" data-rel="tooltip">
                            <span class="glyphicon glyphicon-new-window" aria-hidden="true"> Enviar para Logistica</span> 

    </asp:LinkButton>

          &nbsp;<asp:LinkButton ID="EnviarParaFinanceiroButton" class="btn btn-warning" runat="server" Visible="false" CausesValidation="False"
        OnClick="EnviarParaFinanceiroButton_Click" title="Enviar para Analise Financeiro" data-rel="tooltip">
                            <span class="glyphicon glyphicon-new-window" aria-hidden="true"> Enviar para Analise Financeiro</span> 

    </asp:LinkButton>

        &nbsp;<asp:LinkButton ID="RetornarAdmVendasLinkButton" class="btn btn-warning" runat="server" Visible="false" CausesValidation="False"
        OnClick="RetornarAdmVendasButton_Click" title="Retornar para Adm Vendas Havaliar cadastro" data-rel="tooltip">
                            <span class="glyphicon glyphicon-new-window" aria-hidden="true"> Retornar para Adm Vendas</span> 

    </asp:LinkButton>



    &nbsp;<asp:LinkButton ID="AprovarCadastroFinanceiraButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
         OnClick="AprovarCadastroButton_Click" title="Aprovar Cadastro" data-rel="tooltip">
                            <span class="glyphicon glyphicon-ok" aria-hidden="true"> Aprovar Cadastro</span> 

     </asp:LinkButton>


    &nbsp;
    <asp:LinkButton ID="ReprovarCadastroAdmVendasButton" class="btn btn-danger" runat="server" Visible="false" CausesValidation="False"
        OnClick="InativaCadastroAdmVendasButton_Click" title="Reprovar Cadastro" data-rel="tooltip">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"> Reprovar Cadastro</span> 

    </asp:LinkButton>
        &nbsp;<asp:LinkButton ID="ReprovarCadastroFinanceiroLinkButton" class="btn btn-danger" runat="server" Visible="false" CausesValidation="False"
        OnClick="InativaCadastroFinanceiroButton_Click" title="Reprovar Cadastro" data-rel="tooltip">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"> Reprovar Cadastro</span> 

    </asp:LinkButton>


    &nbsp;<asp:LinkButton ID="CadastroIncompletoLinkButton" class="btn btn-danger" runat="server" Visible="false" CausesValidation="False"
        title="Cadastro Incompleto, recusar Cadastro" data-rel="tooltip" OnClick="CadastroIncompletoLinkButton_Click">
                            <span class="glyphicon glyphicon-remove" aria-hidden="true"> Cadastro Incompleto</span> 

    </asp:LinkButton>


    &nbsp;<asp:LinkButton ID="CadastroCompletoLinkButton" class="btn btn-success" runat="server" Visible="false" CausesValidation="False"
        title="Cadastro Completado" data-rel="tooltip" OnClick="CadastroCompletoLinkButton_Click">
                            <span class="glyphicon glyphicon-ok" aria-hidden="true"> Enviar para Analise Adm Vendas</span> 

    </asp:LinkButton>


        </div>

    <br />
    <br />
    <asp:Literal ID="EntWebSeqEmailXmlLiteral" runat="server" Visible="false"></asp:Literal>
    <asp:Literal ID="ENTCONTATOIDLiteral" runat="server" Visible="false"></asp:Literal>

</asp:Content>
