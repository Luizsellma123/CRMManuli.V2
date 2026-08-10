using System;

namespace Thelus.UI.Engine.Atributos
{
    public enum ActionType
    {
        Create,
        Search,
        Clear,
        Save,
        Back,
        Delete,
        Custom
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ListActionAttribute : Attribute
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; } = "btn-primary";
        public ActionType ActionType { get; set; } = ActionType.Custom;
        public string TargetUrl { get; set; }
        public int Order { get; set; }

        public ListActionAttribute(string label, ActionType actionType)
        {
            Label = label;
            ActionType = actionType;

            // Define ícones e estilos padrão baseados no tipo da ação
            switch (actionType)
            {
                case ActionType.Create:
                    Icon = "mdi mdi-plus";
                    CssClass = "btn-success";
                    Order = 1;
                    break;
                case ActionType.Clear:
                    Icon = "mdi mdi-broom";
                    CssClass = "btn-secondary";
                    Order = 2;
                    break;
                case ActionType.Search:
                    Icon = "mdi mdi-magnify";
                    CssClass = "btn-primary";
                    Order = 3;
                    break;
                case ActionType.Save:
                    Icon = "mdi mdi-content-save";
                    CssClass = "btn-success";
                    Order = 4;
                    break;
                case ActionType.Back:
                    Icon = "mdi mdi-keyboard-return";
                    CssClass = "btn-danger btn-sm";
                    Order = 5;
                    break;
                case ActionType.Delete:
                    Icon = "mdi mdi-trash-can";
                    CssClass = "btn-outline-danger";
                    Order = 6;
                    break;
            }
        }
    }
}