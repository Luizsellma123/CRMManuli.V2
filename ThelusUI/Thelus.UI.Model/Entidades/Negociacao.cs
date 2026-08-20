using System;
using System.Collections.Generic;
using Thelus.UI.Engine.Atributos;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Model.Entidades
{
    // AÇÕES DA TELA DE LISTAGEM
    [ListAction("Nova Negociação", ActionType.Create)]
    [ListAction("Limpar", ActionType.Clear)]
    [ListAction("Buscar", ActionType.Search)]

    // AÇÕES DA SEÇÃO: Identificação (BOTÕES DE AÇÃO NO RODAPÉ DO DETALHE)
    [DetailAction("Retornar", ActionType.Back, Section = "Identificação", CssClass = "btn-danger", Order = 1)]
    [DetailAction("Retornar Negociação", ActionType.Custom, Section = "Identificação", CssClass = "btn-warning", Order = 2)]
    [DetailAction("Perder Venda", ActionType.Custom, Section = "Identificação", CssClass = "btn-warning", Order = 3)]
    [DetailAction("Reabrir", ActionType.Custom, Section = "Identificação", CssClass = "btn-info", Order = 4)]
    [DetailAction("Reprovar", ActionType.Custom, Section = "Identificação", CssClass = "btn-danger", Order = 5)]
    [DetailAction("Aprovar", ActionType.Custom, Section = "Identificação", CssClass = "btn-info", Order = 6)]
    [DetailAction("Gravar", ActionType.Save, Section = "Identificação", CssClass = "btn-success", Order = 7)]

    // CONFIGURA O LAYOUT PARA TELA CHEIA (12 COLUNAS)
    [ListLayout(ListLayoutMode.FullWidth, ShowQuickSearch = false)]
    public class Negociacao
    {
        #region Seção: Identificação

        // LINHA 1 FILTRO (12 Colunas)
        [FormField(Label = "Empresa", Section = "Identificação", ColSpan = 12, Order = 1, FieldType = FieldType.Select, AllowNullOption = false, LookupKey = "acesso-empresas", ShowInFilter = true)]
        public int IdEmpresa { get; set; } = 1;

        // LINHA 2 FILTRO (6 + 6 = 12 Colunas)
        [FormField(Label = "Usuário", Section = "Identificação", ColSpan = 6, Order = 2, FieldType = FieldType.Select, EnableSearch = true, LookupKey = "negociacao-usuarios", ShowInFilter = true)]
        [FormDetailField(ReadOnly = true, DefaultValue = DefaultValueType.CurrentUser)]
        public string Solicitante { get; set; }

        [FormField(Label = "Situação", Section = "Identificação", ColSpan = 6, Order = 3, FieldType = FieldType.Select, LookupKey = "global-status-146", ShowInFilter = true, ShowInGrid = true)]
        [FormDetailField(ReadOnly = true)]
        public int IdSituacao { get; set; } = 1;

        // LINHA 3 FILTRO (6 + 6 = 12 Colunas)
        [FormField(Label = "Data Inicio", Section = "Identificação", ColSpan = 6, Order = 4, FieldType = FieldType.Date, ShowInFilter = true, Mask = "00/00/0000", Placeholder = "00/00/0000")]
        [FormDetailField(Visible = false)]
        public DateTime DataInicio { get; set; } = DateTime.Now;

        [FormField(Label = "Fim", Section = "Identificação", ColSpan = 6, Order = 5, FieldType = FieldType.Date, ShowInFilter = true, Mask = "00/00/0000", Placeholder = "00/00/0000")]
        [FormDetailField(Visible = false)]
        public DateTime DataFim { get; set; } = DateTime.Now;

        // LINHA 4 FILTRO (6 + 6 = 12 Colunas)
        [FormField(Label = "Negociação", Section = "Identificação", ColSpan = 6, Order = 6, ReadOnly = true, ShowInFilter = true, ShowInGrid = true)]
        public int IdNegociacao { get; set; }

        [FormField(Label = "Frete", Section = "Identificação", ColSpan = 6, Order = 7, FieldType = FieldType.Select, LookupKey = "negociacao-frete", ShowInFilter = true)]
        public string Frete { get; set; }

        // LINHA 5 FILTRO (12 Colunas)
        [FormField(Label = "Cliente", Section = "Identificação", ColSpan = 12, Order = 8, IsRequired = true, Placeholder = "Clique na lupa para pesquisar o cliente...", FieldType = FieldType.ModalLookup, LookupKey = "negociacao-clientes", ShowInFilter = true, ShowInGrid = true)]
        public string Cliente { get; set; }

        // PROPRIEDADE EXCLUSIVA DA GRID (Sem exibição no filtro para não quebrar a ordem)
        [FormField(Label = "Data", Section = "Identificação", ColSpan = 3, Order = 9, FieldType = FieldType.Date, ShowInFilter = false, ShowInGrid = true)]
        public DateTime Data { get; set; } = DateTime.Now;

        // CAMPOS EXCLUSIVOS DO FORMULÁRIO DE DETALHE (ShowInFilter = false)
        [FormField(Label = "Estado", Section = "Identificação", ColSpan = 3, Order = 10, FieldType = FieldType.Select, LookupKey = "Estados")]
        public string Estado { get; set; }

        [FormField(Label = "Cidade", Section = "Identificação", ColSpan = 3, Order = 11, FieldType = FieldType.Select, LookupKey = "Cidades")]
        public string Cidade { get; set; }

        [FormField(Label = "Novo", Section = "Identificação", ColSpan = 3, Order = 12)]
        public bool IsNovo { get; set; }

        [FormField(Label = "Cond. Pgto.", Section = "Identificação", ColSpan = 6, Order = 13, FieldType = FieldType.Select, LookupKey = "CondicoesPagamento")]
        public string CondicaoPagamento { get; set; }

        [FormField(Label = "Vendedor", Section = "Identificação", ColSpan = 6, Order = 14, FieldType = FieldType.Select, LookupKey = "Vendedores")]
        public string Vendedor { get; set; }

        [FormField(Label = "Regime", Section = "Identificação", ColSpan = 6, Order = 15, FieldType = FieldType.Select, LookupKey = "RegimesTributarios")]
        public string Regime { get; set; }

        [FormField(Label = "Clas. Comercial.", Section = "Identificação", ColSpan = 6, Order = 16, FieldType = FieldType.Select, LookupKey = "ClassificacoesComerciais")]
        public string ClassificacaoComercial { get; set; }

        [FormField(Label = "Validade", Section = "Identificação", ColSpan = 6, Order = 17, FieldType = FieldType.Select, LookupKey = "Validades")]
        public string Validade { get; set; }

        #endregion

        #region Seção: Observações e Histórico

        [FormField(Label = "Observação", Section = "Observações e Histórico", ColSpan = 12, Order = 18, FieldType = FieldType.TextArea, Placeholder = "Digite as observações da negociação...")]
        public string Observacao { get; set; }

        [FormField(Label = "Histórico", Section = "Observações e Histórico", ColSpan = 12, Order = 19, FieldType = FieldType.TextArea, Placeholder = "Histórico do atendimento...")]
        public string Historico { get; set; }

        #endregion
    }
}