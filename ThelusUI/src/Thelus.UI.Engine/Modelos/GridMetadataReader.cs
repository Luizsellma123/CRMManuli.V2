using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Thelus.UI.Engine.Atributos;

namespace Thelus.UI.Engine.Modelos
{
    public static class GridMetadataReader
    {
        public static List<ColumnMetadata> GetColumns(Type itemType)
        {
            var list = new List<ColumnMetadata>();
            if (itemType == null) return list;

            var properties = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var gridAttr = prop.GetCustomAttribute<GridColumnAttribute>();
                var formAttr = prop.GetCustomAttribute<FormFieldAttribute>();

                // Regra de Visibilidade da Grid:
                // - Se tem [GridColumn], respeita a propriedade Visible.
                // - Se NÃO tem [GridColumn], obriga ter [FormField] com ShowInGrid == true.
                bool isVisibleInGrid = gridAttr != null
                    ? gridAttr.Visible
                    : (formAttr != null && formAttr.ShowInGrid);

                if (!isVisibleInGrid)
                    continue;

                string header = gridAttr?.Header
                                ?? formAttr?.Label
                                ?? prop.Name;

                int order = gridAttr?.Order
                            ?? formAttr?.Order
                            ?? 0;

                string format = gridAttr?.Format
                                ?? formAttr?.Format
                                ?? string.Empty;

                list.Add(new ColumnMetadata
                {
                    PropertyName = prop.Name,
                    PropertyInfo = prop,
                    PropertyType = prop.PropertyType,
                    Header = header,
                    Order = order,
                    Visible = true,
                    Format = format,
                    Width = gridAttr?.Width ?? string.Empty
                });
            }

            return list.OrderBy(c => c.Order).ToList();
        }
    }
}