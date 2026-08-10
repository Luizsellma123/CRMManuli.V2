using System;

namespace Thelus.UI.Engine.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class GridActionAttribute : Attribute
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public ActionType ActionType { get; set; }
        public int Order { get; set; }

        public GridActionAttribute(string label, ActionType actionType)
        {
            Label = label;
            ActionType = actionType;

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
                default:
                    CssClass = "btn-primary";
                    Order = 10;
                    break;
            }
        }
    }
}