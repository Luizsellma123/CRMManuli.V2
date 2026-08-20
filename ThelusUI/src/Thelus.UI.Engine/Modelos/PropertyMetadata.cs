using System;
using System.Collections.Generic;
using System.Reflection;
using Thelus.UI.Engine.Atributos;

namespace Thelus.UI.Engine.Modelos
{
    public class PropertyMetadata
    {
        public string PropertyName { get; set; }
        public Type PropertyType { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public string Label { get; set; }
        public string Section { get; set; }
        public int ColSpan { get; set; }
        public int Order { get; set; }
        public FieldType FieldType { get; set; }

        /// <summary>
        /// Chave para identificar a lista de opções dinâmicas (Lookup) fornecida ao formulário.
        /// </summary>
        public string LookupKey { get; set; }

        /// <summary>
        /// Define o nome da propriedade booleana (ex: "Novo") que alterna este campo para digitação manual/livre.
        /// </summary>
        public string ToggleEditableProperty { get; set; }

        public bool ShowInList { get; set; } = true;

        public string Icon { get; set; }
        public string HelpText { get; set; }
        public string Placeholder { get; set; }
        public bool IsRequired { get; set; }
        public bool ReadOnly { get; set; }
        public bool Visible { get; set; }
        public int Rows { get; set; }
        public string Mask { get; set; }
        public string Format { get; set; }
        public string Pattern { get; set; }
        public string ErrorMessage { get; set; }
        public string Accept { get; set; }
        public object Value { get; set; }
        public bool ShowInFilter { get; set; }

        /// <summary>
        /// Se verdadeiro, exibe este campo na tabela/grid de listagem de registros.
        /// </summary>
        public bool ShowInGrid { get; set; }

        /// <summary>
        /// Define se o campo do tipo Select/Dropdown deve exibir a opção nula ou vazia ("Todas" / "Selecione...").
        /// </summary>
        public bool AllowNullOption { get; set; } = true;

        /// <summary>
        /// Define se o campo do tipo Select/Dropdown deve permitir pesquisa de opções na interface.
        /// </summary>
        public bool EnableSearch { get; set; } = false;

        public string FilterValue { get; set; } = string.Empty;

        #region Configurações Exclusivas de Detalhe (FormDetailField)

        /// <summary>
        /// Define se o campo deve ser exibido no formulário de detalhe.
        /// </summary>
        public bool VisibleInDetail { get; set; } = true;

        /// <summary>
        /// Se verdadeiro, trava o campo para edição na tela de detalhe (tanto na Inclusão quanto na Edição).
        /// </summary>
        public bool ReadOnlyInDetail { get; set; } = false;

        /// <summary>
        /// Se verdadeiro, trava o campo para edição apenas no modo Edição (liberado na Inclusão).
        /// </summary>
        public bool ReadOnlyOnEdit { get; set; } = false;

        /// <summary>
        /// Provedor de valor padrão automático para o detalhe (ex: Usuário Logado, Data Atual).
        /// </summary>
        public DefaultValueType DefaultValueType { get; set; } = DefaultValueType.None;

        #endregion

        #region Mapeamento de Grids e Sub-Entidades

        /// <summary>
        /// Nomes das propriedades do Pai a serem exibidas no cabeçalho de contexto de seções Grid.
        /// </summary>
        public string[] ContextHeaderProps { get; set; }

        /// <summary>
        /// Tipo da classe contida na Lista/Grid (ex: ContatoModel).
        /// </summary>
        public Type ChildType { get; set; }

        #endregion

        /// <summary>
        /// Obtém o valor da propriedade a partir de uma instância.
        /// </summary>
        public object GetValue(object instance)
        {
            if (instance == null || PropertyInfo == null) return null;
            return PropertyInfo.GetValue(instance);
        }

        /// <summary>
        /// Define o valor da propriedade em uma instância.
        /// </summary>
        public void SetValue(object instance, object value)
        {
            if (instance == null || PropertyInfo == null || !PropertyInfo.CanWrite) return;
            PropertyInfo.SetValue(instance, value);
        }

        /// <summary>
        /// Lista de opções (Chave/Valor) para campos do tipo Select/Dropdown
        /// </summary>
        public Dictionary<string, string> Options { get; set; } = new();
    }
}