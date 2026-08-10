using System;

namespace Thelus.UI.Engine.Atributos
{
    public enum ListLayoutMode
    {
        WithSideMenu, // Dividido (9 colunas + 3 colunas para menu lateral) - PADRÃO CRM
        FullWidth     // Tela inteira (12 colunas sem menu lateral)
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ListLayoutAttribute : Attribute
    {
        public ListLayoutMode Mode { get; set; }

        /// <summary>
        /// Define o modo de layout da listagem.
        /// Se usado como [ListLayout], assume automaticamente o modo FullWidth (Tela Cheia).
        /// </summary>
        public ListLayoutAttribute(ListLayoutMode mode = ListLayoutMode.FullWidth)
        {
            Mode = mode;
        }
    }
}