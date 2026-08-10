using Thelus.UI.Engine.Atributos;

namespace Thelus.UI.Model.Entidades
{
    public class StatusModel
    {
        [FormField(Label = "ID", Section = "Geral", ColSpan = 2, Order = 1, ReadOnly = true, ShowInGrid = true)]
        public int IdStatus { get; set; }

        [FormField(Label = "Descrição", Section = "Geral", ColSpan = 8, Order = 2, IsRequired = true, ShowInGrid = true)]
        public string Descricao { get; set; }

        [FormField(Label = "ID Tabela", Section = "Geral", ColSpan = 2, Order = 3, ShowInGrid = false)]
        public int IDTabela { get; set; }
    }
}