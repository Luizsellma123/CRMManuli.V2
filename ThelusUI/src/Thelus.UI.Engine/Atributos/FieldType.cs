namespace Thelus.UI.Engine.Atributos
{
    /// <summary>
    /// Tipos visuais e comportamentais suportados pelo motor Thelus.UI.
    /// </summary>
    public enum FieldType
    {
        /// <summary>Inspeciona o tipo C# da propriedade e decide a melhor renderização.</summary>
        Auto,

        // Campos de Texto Simples e Especiais
        Text,
        TextArea,
        Password,
        Email,
        Phone,
        Url,

        // Campos Numéricos e Monetários
        Number,
        Currency,

        // Data e Hora
        Date,
        Time,
        DateTime,

        // Seleção e Opções
        CheckBox,
        Switch,
        Select,
        Radio,

        // Utilitários Avançados
        Color,
        File,
        Hidden,

        // Coleções e Sub-Entidades
        /// <summary>Representa uma coleção/lista de dados (1 para N) renderizada em formato de Tabela/Grid.</summary>
        Grid
    }
}