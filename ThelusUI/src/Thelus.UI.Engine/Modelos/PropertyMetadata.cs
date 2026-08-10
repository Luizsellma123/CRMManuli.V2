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

        // ADICIONE ESTA PROPRIEDADE PARA A LISTAGEM PRINCIPAL
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

        public string FilterValue { get; set; } = string.Empty;

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