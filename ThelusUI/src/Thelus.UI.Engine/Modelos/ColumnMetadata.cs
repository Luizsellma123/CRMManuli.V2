using System;
using System.Reflection;

namespace Thelus.UI.Engine.Modelos
{
    public class ColumnMetadata
    {
        public string PropertyName { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public Type PropertyType { get; set; }
        public string Header { get; set; }
        public int Order { get; set; }
        public bool Visible { get; set; }
        public string Format { get; set; }
        public string Width { get; set; }
    }
}