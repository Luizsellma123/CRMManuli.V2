using System.Collections.Generic;

namespace Thelus.UI.Engine.Modelos
{
    public class MenuItem
    {
        public int IdMenu { get; set; }
        public string Title { get; set; }

        // Propriedades do Ícone
        public int? IdIcone { get; set; }      // O ID da tabela MENUS_ICONES (útil para edição/vínculo)
        public string Icon { get; set; }        // A classe CSS do ícone para o Front-end renderizar

        public string Url { get; set; } = "javascript:void(0);";
        public string EntityName { get; set; }  // Liga o menu direto à engine genérica (ex: "Usuarios")
        public bool IsTitle { get; set; } = false;
        public List<MenuItem> SubItems { get; set; } = new();
    }
}