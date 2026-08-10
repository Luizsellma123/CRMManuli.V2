using System;

namespace Thelus.UI.Engine.Atributos
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class GridColumnAttribute : Attribute
    {
        public string Header { get; set; }
        public int Order { get; set; } = 0;
        public bool Visible { get; set; } = true;
        public string Format { get; set; } = string.Empty;
        public string Width { get; set; } = string.Empty;
    }
}