using System.Collections.Generic;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;

namespace Thelus.Core.Servicos
{
    public interface IMenuCoreService
    {
        Task<List<MenuItem>> ObterMenusTabelaAsync();
    }
}