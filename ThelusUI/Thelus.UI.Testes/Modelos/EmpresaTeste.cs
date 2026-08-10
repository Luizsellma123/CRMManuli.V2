using System;
using System.Collections.Generic;
using Thelus.UI.Engine.Atributos;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Engine.Modelos
{
    // AÇÕES DA TELA DE LISTAGEM
    [ListAction("Novo Cadastro", ActionType.Create)]
    [ListAction("Limpar", ActionType.Clear)]
    [ListAction("Pesquisar", ActionType.Search)]

    // AÇÕES DA SEÇÃO: Identificação
    [DetailAction("Retornar", ActionType.Back, Section = "Identificação", CssClass = "btn-danger", Order = 1)]
    [DetailAction("Salvar", ActionType.Save, Section = "Identificação", CssClass = "btn-success", Order = 2)]

    // AÇÕES DA SEÇÃO: Endereço e Localização
    [DetailAction("Retornar", ActionType.Back, Section = "Endereço e Localização", CssClass = "btn-danger", Order = 1)]
    [DetailAction("Salvar", ActionType.Save, Section = "Endereço e Localização", CssClass = "btn-success", Order = 2)]

    // AÇÕES DA SEÇÃO: Contatos
    //[DetailAction("Retornar", ActionType.Back, Section = "Contatos", CssClass = "btn-danger", Order = 1)]
    public class EmpresaTeste
    {
        #region Seção: Identificação

        [FormField(Label = "Código", Section = "Identificação", ColSpan = 3, Order = 1, ReadOnly = true, ShowInFilter = true)]
        public int Id { get; set; } = 1;

        [FormField(Label = "Nome Fantasia", Section = "Identificação", ColSpan = 9, Order = 2, IsRequired = true, Placeholder = "Digite o nome fantasia...", ShowInFilter = true)]
        public string NomeFantasia { get; set; } = "MULTI MERCANTES LTDA";

        [FormField(Label = "Data de Cadastro", Section = "Identificação", ColSpan = 6, Order = 3, FieldType = FieldType.Date, ReadOnly = true)]
        public DateTime Cadastro { get; set; } = new DateTime(2025, 3, 14);

        [FormField(Label = "CNPJ", Section = "Identificação", ColSpan = 6, Order = 4, IsRequired = true, Placeholder = "00.000.000/0000-00", Mask = "00.000.000/0000-00", ShowInFilter = true)]
        public string Cnpj { get; set; } = "04.049.640/0001-47";

        [FormField(Label = "CEP", Section = "Identificação", ColSpan = 6, Order = 5, Placeholder = "00000-000", Mask = "00000-000")]
        public string Cep { get; set; } = "83.085-500";

        // Contatos Diretos da Entidade Empresa (Preservados na Identificação)
        [FormField(Label = "E-mail Principal", Section = "Identificação", ColSpan = 6, Order = 6, FieldType = FieldType.Email, Placeholder = "empresa@exemplo.com")]
        public string Email { get; set; } = "";

        [FormField(Label = "Telefone Principal", Section = "Identificação", ColSpan = 6, Order = 7, Placeholder = "(00) 0000-0000", Mask = "(00) 0000-0000")]
        public string Telefone { get; set; } = "(41) 3021-3500";

        [FormField(Label = "Celular Principal", Section = "Identificação", ColSpan = 6, Order = 8, Placeholder = "(00) 00000-0000", Mask = "(00) 00000-0000")]
        public string Celular { get; set; } = "";

        #endregion

        #region Seção: Endereço e Localização

        [FormField(Label = "País", Section = "Endereço e Localização", ColSpan = 6, Order = 9, FieldType = FieldType.Select, LookupKey = "Paises", ContextHeaderProps = new[] { nameof(NomeFantasia), nameof(Cnpj) })]
        public string Pais { get; set; } = "Brasil";

        [FormField(Label = "Estado / UF", Section = "Endereço e Localização", ColSpan = 6, Order = 10, FieldType = FieldType.Select, LookupKey = "Estados")]
        public string Estado { get; set; } = "Paraná";

        [FormField(Label = "Complemento", Section = "Endereço e Localização", ColSpan = 6, Order = 11)]
        public string Complemento { get; set; } = "São José dos Pinhais";

        [FormField(Label = "Município", Section = "Endereço e Localização", ColSpan = 6, Order = 12, FieldType = FieldType.Select, LookupKey = "Municipios")]
        public string Municipio { get; set; } = "São José dos Pinhais";

        [FormField(Label = "Endereço", Section = "Endereço e Localização", ColSpan = 6, Order = 13)]
        public string Endereco { get; set; } = "Rua Joaquim Alves Fontes";

        [FormField(Label = "Número", Section = "Endereço e Localização", ColSpan = 6, Order = 14)]
        public string Numero { get; set; } = "2098";

        [FormField(Label = "Bairro", Section = "Endereço e Localização", ColSpan = 6, Order = 15)]
        public string Bairro { get; set; } = "COLONIA MURICI";

        #endregion

        #region Seção: Contatos (Grid / Sub-Entidades)

        // Tabela 1-para-N de Contatos com cabeçalho de contexto dinâmico vindo da Empresa
        [FormField(
            Label = "Contatos",
            Section = "Contatos",
            FieldType = FieldType.Grid,
            Order = 16,
            ContextHeaderProps = new[] { nameof(NomeFantasia), nameof(Cnpj) }
        )]
        [GridAction("Adicionar Contato", ActionType.Create, CssClass = "btn-success")]
        [GridAction("Limpar Campos", ActionType.Clear, CssClass = "btn-secondary")]
        // Mudar de DetailAction para GridAction:
        [GridAction("Retornar", ActionType.Back, CssClass = "btn-danger", Icon = "mdi mdi-keyboard-return")]
        public List<EmpresaContatoModel> Contatos { get; set; } = new();

        #endregion
    }
}