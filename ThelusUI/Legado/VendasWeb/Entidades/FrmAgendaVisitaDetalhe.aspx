<%@ Page Title="Agenda Visita Destalhes" Language="C#" MasterPageFile="~/CRM.Master"
    AutoEventWireup="true" CodeBehind="FrmAgendaVisitaDetalhe.aspx.cs" Inherits="VendasWeb.Entidades.FrmAgendaVisitaDetalhe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery1.4.1.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery.maskedinput.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/javaScripts/JsMask.js")%>"
        type="text/javascript"></script>
    <script language="javascript" src="<%=Page.ResolveClientUrl("~/js/JsMascarasGerais.js")%>"
        type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- LINHA 1-->
    <div class="row">
        <!-- COLUNA 1-->
        <div class="col-sm-15">
            <!--===================================================-->
            <!--Painel Carteiras e Filtros-->
            <!--===================================================-->
            <div class="panel panel-info">
                <!--Panel heading-->
                <!--Título e controles para o painel de Filtros-->
                <div class="panel-heading">
                    <div class="panel-control">
                        <!--<button type="button" class="demo-panel-ref-btn btn btn-default" data-toggle="panel-overlay"
                            data-target="#filtros">
                            <i class="fa fa-refresh"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-target="#filtros" data-toggle="collapse">
                            <i class="fa fa-chevron-down"></i>
                        </button>
                        <button type="button" class="btn btn-default" data-dismiss="panel">
                            <i class="fa fa-times"></i>
                        </button>-->
                    </div>
                    <h3 class="panel-title">
                        Agenda Visita Detalhes</h3>
                </div>
                <!--Painel Aberto-->
                <!-- END Painel Aberto-->
                <!-- END Painel-->
                <div class="panel-body">

                <!--Filtro Vendedor-->
                <asp:Label ID="VendCodLabel" runat="server" CssClass="text-thin" Text="" Width="130">Escolher Vendedor:</asp:Label>
                <asp:DropDownList ID="VendCodDropDownList" runat="server" CssClass="selectpicker show-tick">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic" SetFocusOnError="True" ControlToValidate="VendCodDropDownList" ErrorMessage="Selecione um Vendedor!"></asp:RequiredFieldValidator>                                      
                
                    <br />
                    <br />
                    <asp:Label ID="AgendaStatusLabel" runat="server" CssClass="text-thin" Text="" Width="130">Status:</asp:Label>
                    <asp:DropDownList ID="AgendaStatusDropDownList" runat="server" CssClass="selectpicker show-tick">
                        <asp:ListItem>Agendada</asp:ListItem>
                        <asp:ListItem>Em Atendimento</asp:ListItem>
                        <asp:ListItem>Finalizada</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="Dynamic"
                        SetFocusOnError="True" ControlToValidate="AgendaStatusDropDownList" ErrorMessage="*"></asp:RequiredFieldValidator>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                    <asp:Label ID="Label2" runat="server" CssClass="text-thin" Text="Data Visita:" Width="100"></asp:Label>
                    <asp:TextBox ID="DataVisitaTextBox" runat="server" TextMode="Date" Width="150px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                        SetFocusOnError="True" ControlToValidate="DataVisitaTextBox" ErrorMessage="*"></asp:RequiredFieldValidator>


                       

                    <br />
                    <br />
                    <asp:Label ID="Label3" runat="server" CssClass="text-thin" Text="CNPJ/CPF:" Width="130"></asp:Label>
                    <asp:TextBox ID="Cnpj_CpfTextBox" runat="server" class="form-control" AutoPostBack="true"
                        onkeypress="mascara( this, mnum );" OnTextChanged="Cnpj_CpfTextBox_TextChanged"
                        Width="151px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Cnpj_CpfTextBox"
                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    
                    <asp:Literal ID="EntCodLiteral" runat="server" Visible="false"></asp:Literal>

                   
                    <br />
                    <br />
                    <asp:Label ID="EntNomeLabel" runat="server" CssClass="text-thin" Text="Nome:" Width="130"></asp:Label>
                    <asp:TextBox ID="EntNomeTextBox" runat="server" class="form-control" MaxLength="100"
                        Width="500px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="EntNomeTextBox"
                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="Label9" runat="server" CssClass="text-thin" Text="UF:" Width="130"></asp:Label>
                    <asp:TextBox ID="UFTextBox" runat="server" class="form-control" MaxLength="2" Width="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="UFTextBox"
                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="Label8" runat="server" CssClass="text-thin" Text="Cidade:" Width="130"></asp:Label>
                    <asp:TextBox ID="CidNomeCompTextBox" runat="server" class="form-control" Width="192px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="CidNomeCompTextBox"
                        Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    <br />
                    <br />
                    <asp:Label ID="Label10" runat="server" CssClass="text-thin" Text="Telefone:" Width="130"></asp:Label>
                    <asp:TextBox ID="TelefoneTextBox" runat="server" class="form-control" onkeypress="mascara( this, mnum );"
                        Width="192px"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" CssClass="text-thin" Text="Observacao: (Max.200)" Width="130"></asp:Label>
                    <asp:TextBox ID="ObservacaoTextBox" runat="server" class="form-control" Width="500px"
                        Height="150px" TextMode="MultiLine" MaxLength="200"></asp:TextBox>


                    <br /><br />
                    <asp:Label ID="Label4" runat="server" CssClass="text-thin" Text="Condição Cliente:" Width="130"></asp:Label>
                    <asp:RadioButtonList ID="CondicaoClienteRadioButtonList" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">Novo</asp:ListItem>
                        <asp:ListItem>Inativo</asp:ListItem>
                        <asp:ListItem>Manutenção</asp:ListItem>
                    </asp:RadioButtonList>


                    <br />
                    <hr />
                    <br />
                    
                     &nbsp;&nbsp;
                    <asp:LinkButton ID="NovoLinkButton" class="btn btn-default" runat="server" title="Novo Produto" 
                        data-rel="tooltip" OnClick="NovoLinkButton_Click"> Novo Produto &raquo;</asp:LinkButton>
                        <br /><br />
                    <div class="table-responsive">
                        <asp:GridView ID="ProdutoVisitaGridView" EmptyDataText="Nenhum Produto Vinculado" AutoGenerateColumns="False"
                            runat="server" EnableModelValidation="True" AllowPaging="True" OnPageIndexChanging="ProdutoVisitaGridView_PageIndexChanged"
                            PageSize="10" CssClass="table table-hover table-striped table-bordered table-checkable table-highlight-head table-no-inner-border table-hover table-condensed"
                            Style="border-collapse: collapse;">
                            <PagerStyle CssClass="pagination-ys" />
                            <Columns>
                                <asp:TemplateField HeaderText="Detalhes/Editar">
                                    <ItemTemplate>
                                        <center>
                                            <asp:LinkButton ID="DetalheButton" class="btn btn-primary" runat="server" OnClick="DetalheButton_Click"
                                                title="Editar/Visualizar" data-rel="tooltip">
                                                            <span class="glyphicon glyphicon-edit center"></span>

                                            </asp:LinkButton>
                                        </center>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Código " Visible="false"> 
                                    <ItemTemplate>
                                        <asp:Label ID="PRODUTO_VISITA_IDLabel" runat="server" Text='<%# Bind("PRODUTO_VISITA_ID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ProdCodEstr" HeaderText="Cód. Produto"></asp:BoundField>
                                <asp:BoundField DataField="ProdNome" HeaderText="Produto"></asp:BoundField>
                                <asp:BoundField DataField="ClasseQtd" HeaderText="Classe Qtd."></asp:BoundField>
                                <asp:BoundField DataField="PrazoPotencialMesCorrente" HeaderText="Prazo P. Mês Corrente"></asp:BoundField>
                                <asp:BoundField DataField="PrazoPotencialMes1" HeaderText="Prazo P. Mês 1"></asp:BoundField>
                                <asp:BoundField DataField="PrazoPotencialMes3" HeaderText="Prazo P. Mês 3"></asp:BoundField>
                                <asp:BoundField DataField="PrazoPotencialMesSuperior" HeaderText="Prazo P. maior que 3 Mês"></asp:BoundField>
                                

                                 <asp:TemplateField HeaderText="Remover">
                    <EditItemTemplate>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <center>
                            <asp:LinkButton ID="RemoverButton" class="btn btn-danger" runat="server" CausesValidation="False"
                                OnClick="RemoverButton_Click" data-rel="tooltip">
                                                               <span class="glyphicon glyphicon-floppy-remove" aria-hidden="true"></span> 

                            </asp:LinkButton>
                        </center>
                    </ItemTemplate>
                    <HeaderStyle CssClass="tabLstCab th" />
                    <ItemStyle CssClass="text-align-center" />
                </asp:TemplateField>


                            </Columns>
                        </asp:GridView>
                    </div>


                </div>
                <!--===================================================-->
                <!-- Panel Footer-->
                <!-- -->
                <!--===================================================-->
                <div class="panel-footer">
                    <div class="row">
                        <div class="panel-control">


                        <asp:LinkButton ID="VoltarLinkButton" class="btn btn-danger" runat="server" title="Voltar/Cancelar" CausesValidation="false"
                                data-rel="tooltip" OnClick="VoltarLinkButton_Click">
        
    <span class="glyphicon glyphicon-arrow-left" aria-hidden="true"> Voltar</span>
    
                            </asp:LinkButton>



                            &nbsp;&nbsp;



                            <asp:LinkButton ID="SalvarLinkButton" class="btn btn-success" runat="server" title="Salvar"
                                data-rel="tooltip" OnClick="SalvarLinkButton_Click">
        
    <span class="glyphicon glyphicon-floppy-saved" aria-hidden="true"> Salvar</span>
    
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!--===================================================-->
        <!--End Painel-->
        <!--===================================================-->
    </div>
</asp:Content>
