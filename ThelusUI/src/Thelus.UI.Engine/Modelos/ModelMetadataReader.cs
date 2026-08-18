using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Thelus.UI.Engine.Atributos;

namespace Thelus.UI.Engine.Modelos
{
    public static class ModelMetadataReader
    {
        // 1. Overload para quando for passada uma INSTÂNCIA
        public static List<PropertyMetadata> GetProperties(object model)
        {
            if (model == null) return new List<PropertyMetadata>();

            // Se o objeto já for do tipo 'Type', redireciona corretamente
            if (model is Type t) return GetProperties(t);

            return GetProperties(model.GetType());
        }

        // 2. MÉTODO PRINCIPAL
        public static List<PropertyMetadata> GetProperties(Type type)
        {
            var list = new List<PropertyMetadata>();
            if (type == null) return list;

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                var attr = prop.GetCustomAttribute<FormFieldAttribute>();
                var detailAttr = prop.GetCustomAttribute<FormDetailFieldAttribute>();

                if (attr == null) continue; // Ignora propriedades sem o atributo base FormField

                var propType = prop.PropertyType;
                var nullableType = Nullable.GetUnderlyingType(propType);
                var actualType = nullableType ?? propType;

                var computedFieldType = attr.FieldType;
                Type childType = null;

                // Identifica se a propriedade é uma Lista/Coleção (Grid)
                if (typeof(IEnumerable).IsAssignableFrom(actualType) && actualType != typeof(string))
                {
                    computedFieldType = FieldType.Grid;

                    if (actualType.IsGenericType)
                    {
                        childType = actualType.GetGenericArguments()[0];
                    }
                    else if (actualType.IsArray)
                    {
                        childType = actualType.GetElementType();
                    }
                }
                // Resolução inteligente do tipo visual quando for 'Auto'
                else if (computedFieldType == FieldType.Auto)
                {
                    if (actualType == typeof(bool)) computedFieldType = FieldType.CheckBox;
                    else if (actualType == typeof(DateTime)) computedFieldType = FieldType.Date;
                    else if (actualType == typeof(TimeSpan)) computedFieldType = FieldType.Time;
                    else if (actualType.IsEnum) computedFieldType = FieldType.Select;
                    else if (IsNumericType(actualType)) computedFieldType = FieldType.Number;
                    else computedFieldType = FieldType.Text;
                }

                list.Add(new PropertyMetadata
                {
                    PropertyName = prop.Name,
                    PropertyType = prop.PropertyType,
                    PropertyInfo = prop,
                    Label = attr.Label ?? prop.Name,

                    // FALLBACK DE LAYOUT (Se FormDetailField foi informado e tem valor, usa dele. Senão, herda do FormField)
                    Section = !string.IsNullOrEmpty(detailAttr?.Section) ? detailAttr.Section : attr.Section,
                    ColSpan = (detailAttr?.ColSpan > 0) ? detailAttr.ColSpan : attr.ColSpan,
                    Order = (detailAttr?.Order > 0) ? detailAttr.Order : attr.Order,

                    FieldType = computedFieldType,
                    LookupKey = attr.LookupKey,
                    Icon = attr.Icon,
                    HelpText = attr.HelpText,
                    Placeholder = attr.Placeholder,
                    IsRequired = attr.IsRequired,
                    ReadOnly = attr.ReadOnly,
                    Visible = attr.Visible,
                    ShowInFilter = attr.ShowInFilter,

                    // REGISTROS DE VISIBILIDADE:
                    ShowInList = attr.ShowInList, // Flag para a tabela principal (GenericList)
                    ShowInGrid = attr.ShowInGrid, // Flag para sub-grids de detalhe

                    // REGRAS EXCLUSIVAS PARA A TELA DE DETALHE (FormDetailField)
                    VisibleInDetail = detailAttr != null ? detailAttr.Visible : attr.Visible,
                    ReadOnlyInDetail = detailAttr?.ReadOnly ?? attr.ReadOnly,
                    ReadOnlyOnEdit = detailAttr?.ReadOnlyOnEdit ?? false,
                    DefaultValueType = detailAttr?.DefaultValue ?? DefaultValueType.None,

                    // MAPEAMENTO PARA O SELECT:
                    AllowNullOption = attr.AllowNullOption,
                    EnableSearch = attr.EnableSearch,

                    Rows = attr.Rows,
                    Mask = attr.Mask,
                    Format = attr.Format,
                    Pattern = attr.Pattern,
                    ErrorMessage = attr.ErrorMessage,
                    Accept = attr.Accept,

                    // Mapeamentos para Grids e Contexto do Pai
                    ContextHeaderProps = attr.ContextHeaderProps,
                    ChildType = childType
                });
            }

            // Ordena os campos respeitando a propriedade Order declarada
            return list.OrderBy(p => p.Order).ToList();
        }

        public static List<PropertyMetadata> GetChildProperties(Type childType)
        {
            return GetProperties(childType);
        }

        private static bool IsNumericType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static List<ActionMetadata> GetListActions(Type entityType, string entityName)
        {
            var actions = new List<ActionMetadata>();
            if (entityType == null) return actions;

            var attrs = entityType.GetCustomAttributes<ListActionAttribute>().ToList();

            if (attrs.Any())
            {
                foreach (var attr in attrs)
                {
                    var targetUrl = attr.TargetUrl;

                    if (attr.ActionType == ActionType.Create && string.IsNullOrEmpty(targetUrl))
                    {
                        targetUrl = $"/gerenciar/{entityName}/novo";
                    }

                    var cssClass = attr.CssClass;
                    if (string.IsNullOrEmpty(cssClass))
                    {
                        cssClass = attr.ActionType switch
                        {
                            ActionType.Create => "btn-success",
                            ActionType.Search => "btn-primary",
                            ActionType.Clear => "btn-secondary",
                            _ => "btn-light"
                        };
                    }

                    var icon = attr.Icon;
                    if (string.IsNullOrEmpty(icon))
                    {
                        icon = attr.ActionType switch
                        {
                            ActionType.Create => "mdi mdi-plus",
                            ActionType.Search => "mdi mdi-magnify",
                            ActionType.Clear => "mdi mdi-broom",
                            _ => ""
                        };
                    }

                    actions.Add(new ActionMetadata
                    {
                        Label = attr.Label,
                        Icon = icon,
                        CssClass = cssClass,
                        ActionType = attr.ActionType,
                        TargetUrl = targetUrl,
                        Order = attr.Order
                    });
                }
            }
            else
            {
                actions.Add(new ActionMetadata
                {
                    Label = "Novo Cadastro",
                    Icon = "mdi mdi-plus",
                    CssClass = "btn-success",
                    ActionType = ActionType.Create,
                    TargetUrl = $"/gerenciar/{entityName}/novo",
                    Order = 1
                });
                actions.Add(new ActionMetadata
                {
                    Label = "Limpar",
                    Icon = "mdi mdi-broom",
                    CssClass = "btn-secondary",
                    ActionType = ActionType.Clear,
                    Order = 2
                });
                actions.Add(new ActionMetadata
                {
                    Label = "Pesquisar",
                    Icon = "mdi mdi-magnify",
                    CssClass = "btn-primary",
                    ActionType = ActionType.Search,
                    Order = 3
                });
            }

            return actions.OrderBy(a => a.Order).ToList();
        }

        public static List<ActionMetadata> GetDetailActions(Type entityType, string entityName)
        {
            var actions = new List<ActionMetadata>();
            if (entityType == null) return actions;

            var attrs = entityType.GetCustomAttributes<DetailActionAttribute>().ToList();

            if (attrs.Any())
            {
                foreach (var attr in attrs)
                {
                    var targetUrl = attr.TargetUrl;
                    if (attr.ActionType == ActionType.Back && string.IsNullOrEmpty(targetUrl))
                    {
                        targetUrl = $"/gerenciar/{entityName}";
                    }

                    var cssClass = attr.CssClass;
                    if (string.IsNullOrEmpty(cssClass))
                    {
                        cssClass = attr.ActionType switch
                        {
                            ActionType.Save => "btn-success",
                            ActionType.Back => "btn-danger",
                            _ => "btn-secondary"
                        };
                    }

                    var icon = attr.Icon;
                    if (string.IsNullOrEmpty(icon))
                    {
                        icon = attr.ActionType switch
                        {
                            ActionType.Save => "mdi mdi-content-save",
                            ActionType.Back => "mdi mdi-keyboard-return",
                            _ => ""
                        };
                    }

                    actions.Add(new ActionMetadata
                    {
                        Label = attr.Label,
                        Icon = icon,
                        CssClass = cssClass,
                        ActionType = attr.ActionType,
                        TargetUrl = targetUrl,
                        Order = attr.Order,
                        Section = attr.Section
                    });
                }
            }
            else
            {
                actions.Add(new ActionMetadata
                {
                    Label = "Retornar",
                    Icon = "mdi mdi-keyboard-return",
                    CssClass = "btn-danger",
                    ActionType = ActionType.Back,
                    TargetUrl = $"/gerenciar/{entityName}",
                    Order = 1
                });
                actions.Add(new ActionMetadata
                {
                    Label = "Salvar",
                    Icon = "mdi mdi-content-save",
                    CssClass = "btn-success",
                    ActionType = ActionType.Save,
                    Order = 2
                });
            }

            return actions.OrderBy(a => a.Order).ToList();
        }

        public static List<ActionMetadata> GetGridActions(PropertyInfo prop)
        {
            var actions = new List<ActionMetadata>();
            if (prop == null) return actions;

            var attrs = prop.GetCustomAttributes<GridActionAttribute>().ToList();

            if (attrs.Any())
            {
                foreach (var attr in attrs)
                {
                    var cssClass = attr.CssClass;
                    if (string.IsNullOrEmpty(cssClass))
                    {
                        cssClass = attr.ActionType switch
                        {
                            ActionType.Create => "btn-success",
                            ActionType.Clear => "btn-secondary",
                            _ => "btn-primary"
                        };
                    }

                    var icon = attr.Icon;
                    if (string.IsNullOrEmpty(icon))
                    {
                        icon = attr.ActionType switch
                        {
                            ActionType.Create => "mdi mdi-plus",
                            ActionType.Clear => "mdi mdi-broom",
                            _ => ""
                        };
                    }

                    actions.Add(new ActionMetadata
                    {
                        Label = attr.Label,
                        Icon = icon,
                        CssClass = cssClass,
                        ActionType = attr.ActionType,
                        Order = attr.Order
                    });
                }
            }
            else
            {
                actions.Add(new ActionMetadata
                {
                    Label = "Adicionar",
                    Icon = "mdi mdi-plus",
                    CssClass = "btn-success",
                    ActionType = ActionType.Create,
                    Order = 1
                });
            }

            return actions.OrderBy(a => a.Order).ToList();
        }
    }
}