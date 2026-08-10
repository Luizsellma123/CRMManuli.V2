using System;
using System.Collections.Generic;
using Thelus.UI.Engine.Atributos;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Model.Entidades
{
    // AÇÕES DA TELA DE LISTAGEM
    [ListAction("Novo Cadastro", ActionType.Create)]
    [ListAction("Limpar", ActionType.Clear)]
    [ListAction("Pesquisar", ActionType.Search)]

    // AÇÕES DA SEÇÃO: Identificação
    [DetailAction("Retornar", ActionType.Back, Section = "Identificação", CssClass = "btn-danger", Order = 1)]
    [DetailAction("Salvar", ActionType.Save, Section = "Identificação", CssClass = "btn-success", Order = 2)]

    // AÇÕES DA SEÇÃO: Segurança e Acesso
    [DetailAction("Retornar", ActionType.Back, Section = "Segurança e Acesso", CssClass = "btn-danger", Order = 1)]
    [DetailAction("Salvar", ActionType.Save, Section = "Segurança e Acesso", CssClass = "btn-success", Order = 2)]

    // AÇÕES DA SEÇÃO: Auditoria
    [DetailAction("Retornar", ActionType.Back, Section = "Auditoria", CssClass = "btn-danger", Order = 1)]

    // CONFIGURA O LAYOUT PARA TELA CHEIA (12 COLUNAS)
    [ListLayout(ListLayoutMode.FullWidth)]
    public class UsuarioTeste
    {
        #region Seção: Identificação

        // 1ª Coluna na Grid: Código (ID Numérico - 0 para novos registros)
        [FormField(Label = "Código", Section = "Identificação", ColSpan = 3, Order = 1, ReadOnly = true, ShowInFilter = true, ShowInGrid = true)]
        public int IdUsuario { get; set; }

        // 2ª Coluna na Grid: Código Usuário (Login/Alias)
        [FormField(Label = "Código Usuário", Section = "Identificação", ColSpan = 3, Order = 2, Placeholder = "Código...", ShowInFilter = true, ShowInGrid = true)]
        public string Codigo { get; set; }

        // 3ª Coluna na Grid: Usuário (Nome Completo)
        [FormField(Label = "Usuário", Section = "Identificação", ColSpan = 6, Order = 3, IsRequired = true, Placeholder = "Digite o nome completo...", ShowInFilter = true, ShowInGrid = true)]
        public string Nome { get; set; }

        [FormField(Label = "CPF", Section = "Identificação", ColSpan = 6, Order = 4, IsRequired = true, Placeholder = "000.000.000-00", Mask = "000.000.000-00", ShowInFilter = true)]
        public string Cpf { get; set; }

        [FormField(Label = "Data de Cadastro", Section = "Identificação", ColSpan = 6, Order = 5, FieldType = FieldType.Date, ReadOnly = true)]
        public DateTime Cadastro { get; set; } = DateTime.Now;

        // 4ª Coluna na Grid: Email
        [FormField(Label = "Email", Section = "Identificação", ColSpan = 6, Order = 6, FieldType = FieldType.Email, Placeholder = "usuario@exemplo.com", ShowInFilter = true, ShowInGrid = true)]
        public string Email { get; set; }

        [FormField(Label = "Telefone", Section = "Identificação", ColSpan = 6, Order = 7, Placeholder = "(00) 0000-0000", Mask = "(00) 0000-0000")]
        public string Telefone { get; set; }

        [FormField(Label = "Celular", Section = "Identificação", ColSpan = 6, Order = 8, Placeholder = "(00) 00000-0000", Mask = "(00) 00000-0000")]
        public string Celular { get; set; }

        #endregion

        #region Seção: Segurança e Acesso

        [FormField(Label = "Senha", Section = "Segurança e Acesso", ColSpan = 6, Order = 9, FieldType = FieldType.Password, Placeholder = "Digite a senha...")]
        public string Senha { get; set; }

        [FormField(Label = "ID Tabela / Perfil", Section = "Segurança e Acesso", ColSpan = 6, Order = 10, FieldType = FieldType.Select, LookupKey = "Perfis")]
        public int IdTabela { get; set; } = 1;

        // 5ª Coluna na Grid: Status (Padrão 1 = Ativo)
        [FormField(Label = "Status", Section = "Segurança e Acesso", ColSpan = 6, Order = 11, FieldType = FieldType.Select, LookupKey = "Status", ShowInGrid = true)]
        public int IdStatus { get; set; } = 1;

        #endregion

        #region Seção: Auditoria

        [FormField(Label = "Usuário Inclusão", Section = "Auditoria", ColSpan = 6, Order = 12, ReadOnly = true)]
        public string UsuarioInclusao { get; set; }

        [FormField(Label = "Data Inclusão", Section = "Auditoria", ColSpan = 6, Order = 13, FieldType = FieldType.Date, ReadOnly = true)]
        public DateTime DataInclusao { get; set; } = DateTime.Now;

        [FormField(Label = "Usuário Alteração", Section = "Auditoria", ColSpan = 6, Order = 14, ReadOnly = true)]
        public string UsuarioAlteracao { get; set; }

        [FormField(Label = "Data Alteração", Section = "Auditoria", ColSpan = 6, Order = 15, FieldType = FieldType.Date, ReadOnly = true)]
        public DateTime? DataAlteracao { get; set; }

        #endregion
    }
}