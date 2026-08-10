using System;
using System.Collections.Generic;

namespace Thelus.UI.Engine.Servicos
{
    public static class EntityRegistry
    {
        private static readonly Dictionary<string, Type> _entities = new(StringComparer.OrdinalIgnoreCase);

        public static void Register<T>(string routeName)
        {
            _entities[routeName] = typeof(T);
        }

        public static Type GetEntityType(string routeName)
        {
            return _entities.TryGetValue(routeName, out var type) ? type : null;
        }

        public static IEnumerable<string> GetAllEntities() => _entities.Keys;
    }
}