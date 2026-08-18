using System;

namespace Thelus.UI.Engine.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class FormFieldAttribute : Attribute
    {
        #region 1. Layout e Identificação Visual

        /// <summary>
        /// Rótulo exibido acima ou ao lado do campo.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Nome da seção/agrupador no formulário. Padrão: "Dados Principais".
        /// </summary>
        public string Section { get; set; } = "Dados Principais";

        /// <summary>
        /// Largura no Grid Bootstrap (1 a 12 colunas). Padrão: 12.
        /// </summary>
        public int ColSpan { get; set; } = 12;

        /// <summary>
        /// Ordem de exibição do campo dentro da seção.
        /// </summary>
        public int Order { get; set; } = 0;

        /// <summary>
        /// Tipo do controle visual a ser renderizado na tela.
        /// </summary>
        public FieldType FieldType { get; set; } = FieldType.Auto;

        /// <summary>
        /// Chave para identificar a lista de opções dinâmicas (Lookup) fornecida ao formulário.
        /// </summary>
        public string LookupKey { get; set; } = string.Empty;

        /// <summary>
        /// Classe do ícone decorativo (ex: "fas fa-user", "bi bi-envelope").
        /// </summary>
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// Texto de ajuda ou dica exibido abaixo do campo.
        /// </summary>
        public string HelpText { get; set; } = string.Empty;

        #endregion

        #region 2. Estado e Comportamento

        /// <summary>
        /// Texto de orientação exibido quando o campo estiver vazio.
        /// </summary>
        public string Placeholder { get; set; } = string.Empty;

        /// <summary>
        /// Se verdadeiro, exige preenchimento obrigatório.
        /// </summary>
        public bool IsRequired { get; set; } = false;

        /// <summary>
        /// Se verdadeiro, bloqueia o campo para edição.
        /// </summary>
        public bool ReadOnly { get; set; } = false;

        /// <summary>
        /// Se falso, oculta o campo da interface do usuário.
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Se verdadeiro, exibe este campo no painel de filtros na tela de listagem.
        /// </summary>
        public bool ShowInFilter { get; set; } = false;

        /// <summary>
        /// Se verdadeiro, exibe este campo na tabela/grid de listagem de registros.
        /// </summary>
        public bool ShowInGrid { get; set; } = false;

        /// <summary>
        /// Se verdadeiro, exibe este campo na tabela da listagem principal (GenericList).
        /// </summary>
        public bool ShowInList { get; set; } = true;

        /// <summary>
        /// Define se o campo do tipo Select/Dropdown deve exibir a opção nula ou vazia ("Todas" / "Selecione...").
        /// O valor padrão é true. Defina como false para ocultar a opção nula e forçar a seleção de um item válido.
        /// </summary>
        public bool AllowNullOption { get; set; } = true;

        /// <summary>
        /// Quando verdadeiro, ativa a barra de pesquisa dentro do controle Select/Dropdown.
        /// Padrão: false (renderiza um select HTML simples).
        /// </summary>
        public bool EnableSearch { get; set; } = false;

        #endregion

        #region 3. Formatação, Máscaras e Regras

        /// <summary>
        /// Quantidade de linhas para campos do tipo TextArea. Padrão: 3.
        /// </summary>
        public int Rows { get; set; } = 3;

        /// <summary>
        /// Máscara de digitação (ex: "000.000.000-00", "(00) 00000-0000").
        /// </summary>
        public string Mask { get; set; } = string.Empty;

        /// <summary>
        /// Formato de exibição para valores/datas (ex: "C2", "dd/MM/yyyy").
        /// </summary>
        public string Format { get; set; } = string.Empty;

        /// <summary>
        /// Expressão Regular (Regex) para validação do conteúdo.
        /// </summary>
        public string Pattern { get; set; } = string.Empty;

        /// <summary>
        /// Mensagem de erro personalizada para falhas de validação.
        /// </summary>
        public string ErrorMessage { get; set; } = string.Empty;

        /// <summary>
        /// Extensões de arquivo permitidas para campos File (ex: ".pdf,.png").
        /// </summary>
        public string Accept { get; set; } = string.Empty;

        #endregion

        #region 4. Integração e Sub-Entidades (Grids)

        /// <summary>
        /// Nomes das propriedades da entidade Pai (ex: nameof(NomeFantasia), nameof(Status)) 
        /// que devem ser exibidas em modo leitura no topo de seções do tipo Grid.
        /// </summary>
        public string[] ContextHeaderProps { get; set; }

        #endregion
    }
}