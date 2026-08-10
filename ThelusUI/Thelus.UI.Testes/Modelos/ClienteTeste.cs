using System;
using Thelus.UI.Engine.Atributos;

namespace Thelus.UI.Testes.Modelos
{
    public enum TipoPessoa
    {
        Fisica,
        Juridica
    }

    // Configura a listagem desta entidade com o Menu Lateral Direito (9 colunas + 3 colunas)
    [ListLayout(ListLayoutMode.FullWidth)]
    public class ClienteTeste
    {
        [FormField(Label = "Código", Section = "Identificação", ColSpan = 3, Order = 1, ReadOnly = true)]
        public int Id { get; set; } = 101;

        [FormField(Label = "Tipo de Pessoa", Section = "Identificação", ColSpan = 3, Order = 2, IsRequired = true)]
        public TipoPessoa Tipo { get; set; } = TipoPessoa.Juridica;

        // NOVO CAMPO: Vai carregar os estados do dicionário "Estados" que criamos no Index.razor
        [FormField(Label = "Estado / UF", Section = "Identificação", ColSpan = 6, Order = 3, FieldType = FieldType.Select, LookupKey = "Estados", IsRequired = true)]
        public string Estado { get; set; } = "PR";

        [FormField(Label = "Razão Social", Section = "Identificação", ColSpan = 12, Order = 4, IsRequired = true, Placeholder = "Digite a razão social...")]
        public string RazaoSocial { get; set; } = "Empresa Exemplo LTDA";

        [FormField(Label = "E-mail Principal", Section = "Contato & Acesso", ColSpan = 6, FieldType = FieldType.Email, HelpText = "Utilizado para envio de notas fiscais.")]
        public string Email { get; set; } = "contato@empresa.com.br";

        [FormField(Label = "Senha de Acesso", Section = "Contato & Acesso", ColSpan = 6, FieldType = FieldType.Password, Placeholder = "******")]
        public string Senha { get; set; } = "123456";

        [FormField(Label = "Notificações por SMS", Section = "Configurações", ColSpan = 6, FieldType = FieldType.Switch)]
        public bool NotificarSms { get; set; } = true;

        [FormField(Label = "Cliente Ativo", Section = "Configurações", ColSpan = 6, FieldType = FieldType.CheckBox)]
        public bool Ativo { get; set; } = true;
    }
}