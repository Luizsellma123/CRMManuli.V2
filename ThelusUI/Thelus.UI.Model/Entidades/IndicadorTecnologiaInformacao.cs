using System;
using System.Collections.Generic;
using Thelus.UI.Engine.Atributos;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Model.Entidades
{
    // AÇÕES DA TELA DE INDICADORES (RODAPÉ DO PAINEL DE FILTROS)
    [ListAction("Retornar", ActionType.Back)]
    //[ListAction("Relatorio PDF", ActionType.Export)]
    [ListAction("Buscar", ActionType.Search)]

    // CONFIGURA O LAYOUT PARA TELA CHEIA (OCULTA O MENU LATERAL DIREITO)
    [ListLayout(ListLayoutMode.FullWidth)]
    public class IndicadorTecnologiaInformacao
    {
        #region Filtros de Pesquisa (Exibidos no Painel Superior)

        [FormField(Label = "Responsável", Section = "Filtros", ColSpan = 6, Order = 1, FieldType = FieldType.Select, LookupKey = "Responsaveis", ShowInFilter = true)]
        public int? IdResponsavel { get; set; }

        [FormField(Label = "Solicitante", Section = "Filtros", ColSpan = 6, Order = 2, FieldType = FieldType.Select, LookupKey = "Solicitantes", ShowInFilter = true)]
        public int? IdSolicitante { get; set; }

        [FormField(Label = "Data Inicial", Section = "Filtros", ColSpan = 6, Order = 3, FieldType = FieldType.Date, ShowInFilter = true)]
        public DateTime? DataInicial { get; set; }

        [FormField(Label = "Data Final", Section = "Filtros", ColSpan = 6, Order = 4, FieldType = FieldType.Date, ShowInFilter = true)]
        public DateTime? DataFinal { get; set; }

        [FormField(Label = "Sistema", Section = "Filtros", ColSpan = 12, Order = 5, FieldType = FieldType.Select, LookupKey = "Sistemas", ShowInFilter = true)]
        public int? IdSistemaFiltro { get; set; }

        #endregion

        #region Colunas Exibidas no Grid (Apenas as marcadas com ShowInGrid = true)

        [FormField(Label = "Sistema", Order = 1, ShowInGrid = true)]
        public string Sistema { get; set; }

        [FormField(Label = "Abertas", Order = 2, ShowInGrid = true)]
        public int Abertas { get; set; }

        [FormField(Label = "Finalizadas", Order = 3, ShowInGrid = true)]
        public int Finalizadas { get; set; }

        [FormField(Label = "Homologadas", Order = 4, ShowInGrid = true)]
        public int Homologadas { get; set; }

        [FormField(Label = "Total Ano", Order = 5, ShowInGrid = true)]
        public int TotalAno { get; set; }

        [FormField(Label = "Finalizadas Ano", Order = 6, ShowInGrid = true)]
        public int FinalizadasAno { get; set; }

        [FormField(Label = "Homologadas Ano", Order = 7, ShowInGrid = true)]
        public int HomologadasAno { get; set; }

        #endregion
    }
}