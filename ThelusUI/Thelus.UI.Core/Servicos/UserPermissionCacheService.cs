using System.Collections.Generic;
using System.Linq;

namespace Thelus.Core.Servicos
{
    public class UserPermissionCacheService
    {
        private List<int> _idsMenuPermitidos = new();

        public void CarregarPermissoes(IEnumerable<int> idsMenu)
        {
            _idsMenuPermitidos = idsMenu?.ToList() ?? new List<int>();
        }

        public List<int> ObterIdsMenuPermitidos()
        {
            return _idsMenuPermitidos;
        }

        public bool PossuiPermissoesCarregadas()
        {
            return _idsMenuPermitidos.Any();
        }
    }
}