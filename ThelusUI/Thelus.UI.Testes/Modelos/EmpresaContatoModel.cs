using Thelus.UI.Engine.Atributos;

namespace Thelus.UI.Engine.Modelos // ajuste a namespace de acordo com a sua pasta de modelos
{
    public class EmpresaContatoModel
    {
        [FormField(
            Label = "Nome do Contato",
            ColSpan = 12,
            Order = 1,
            IsRequired = true,
            Placeholder = "Digite o nome completo..."
        )]
        public string Nome { get; set; }

        [FormField(
            Label = "Telefone",
            ColSpan = 6,
            Order = 2,
            Placeholder = "(00) 0000-0000",
            Mask = "(00) 0000-0000"
        )]
        public string Telefone { get; set; }

        [FormField(
            Label = "Celular",
            ColSpan = 6,
            Order = 3,
            Placeholder = "(00) 00000-0000",
            Mask = "(00) 00000-0000"
        )]
        public string Celular { get; set; }

        [FormField(
            Label = "E-mail",
            ColSpan = 6,
            Order = 4,
            FieldType = FieldType.Email,
            Placeholder = "email@exemplo.com"
        )]
        public string Email { get; set; }

        [FormField(
            Label = "Tipo",
            ColSpan = 6,
            Order = 5,
            FieldType = FieldType.Select,
            LookupKey = "TiposContato"
        )]
        public string Tipo { get; set; } = "Comercial";
    }
}