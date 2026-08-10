using System;

namespace Thelus.UI.Engine.Atributos
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DetailActionAttribute : Attribute
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public ActionType ActionType { get; set; } = ActionType.Custom;
        public string TargetUrl { get; set; }
        public int Order { get; set; }
        public string Section { get; set; } // <--- PROPRIEDADE ADICIONADA PARA VINCULAR À SEÇÃO

        public DetailActionAttribute(string label, ActionType actionType)
        {
            Label = label;
            ActionType = actionType;

            switch (actionType)
            {
                case ActionType.Back:
                    Icon = "mdi mdi-keyboard-return";
                    CssClass = "btn-danger";
                    Order = 1;
                    break;
                case ActionType.Save:
                    Icon = "mdi mdi-content-save";
                    CssClass = "btn-success";
                    Order = 2;
                    break;
                case ActionType.Delete:
                    Icon = "mdi mdi-trash-can";
                    CssClass = "btn-outline-danger";
                    Order = 3;
                    break;
                default:
                    CssClass = "btn-secondary";
                    Order = 10;
                    break;
            }
        }
    }
}