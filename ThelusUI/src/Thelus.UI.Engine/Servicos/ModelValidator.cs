using System;
using System.Linq;
using Thelus.UI.Engine.Atributos;

namespace Thelus.UI.Engine.Servicos
{
    public static class ModelValidator
    {
        public static (bool IsValid, string Message) Validar(object item)
        {
            if (item == null) return (false, "Nenhum dado fornecido.");

            var properties = item.GetType().GetProperties();

            foreach (var prop in properties)
            {
                // Pega o atributo [FormField] da propriedade
                var attr = prop.GetCustomAttributes(typeof(FormFieldAttribute), true)
                               .FirstOrDefault() as FormFieldAttribute;

                // Se o campo for obrigatório (IsRequired = true)
                if (attr != null && attr.IsRequired)
                {
                    var value = prop.GetValue(item);
                    var stringValue = value?.ToString()?.Trim();

                    // Valida se está nulo, vazio ou se for inteiro zerado (ex: IdTabela = 0)
                    if (value == null || string.IsNullOrWhiteSpace(stringValue) || (value is int intVal && intVal == 0))
                    {
                        return (false, $"O campo '{attr.Label}' é obrigatório.");
                    }
                }
            }

            return (true, string.Empty);
        }
    }
}