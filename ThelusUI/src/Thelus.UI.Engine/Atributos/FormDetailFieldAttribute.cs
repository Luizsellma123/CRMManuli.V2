using System;

namespace Thelus.UI.Engine.Atributos
{
    public enum DefaultValueType
    {
        None = 0,
        CurrentUser = 1,
        CurrentDate = 2,
        CurrentDateTime = 3
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormDetailFieldAttribute : Attribute
    {
        #region 1. Sobrescrita de Layout no Detalhe

        /// <summary>
        /// Nome da seção/agrupador exclusivo para o formulário de detalhe.
        /// Se não informado, herda o Section do FormField.
        /// </summary>
        public string Section { get; set; } = string.Empty;

        /// <summary>
        /// Largura no Grid Bootstrap exclusiva para o Detalhe (1 a 12 colunas).
        /// Se definido como 0, herda o ColSpan do FormField.
        /// </summary>
        public int ColSpan { get; set; } = 0;

        /// <summary>
        /// Ordem de exibição dentro da seção na tela de detalhe.
        /// Se definido como 0, herda o Order do FormField.
        /// </summary>
        public int Order { get; set; } = 0;

        #endregion

        #region 2. Estado e Visibilidade Exclusivos do Detalhe

        /// <summary>
        /// Se falso, oculta este campo na tela de detalhe (ideal para campos de filtros de busca).
        /// Padrão: true.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Se verdadeiro, trava o campo para edição SEMPRE no Detalhe (tanto na Inclusão quanto na Alteração).
        /// </summary>
        public bool ReadOnly { get; set; } = false;

        /// <summary>
        /// Se verdadeiro, trava o campo para edição APENAS no modo Alteração (liberado na Inclusão).
        /// </summary>
        public bool ReadOnlyOnEdit { get; set; } = false;

        #endregion

        #region 3. Auto-Preenchimento no Detalhe

        /// <summary>
        /// Define o provedor de valor padrão automático caso o campo esteja vazio (ex: Usuário Logado, Data Atual).
        /// </summary>
        public DefaultValueType DefaultValue { get; set; } = DefaultValueType.None;

        #endregion
    }
}