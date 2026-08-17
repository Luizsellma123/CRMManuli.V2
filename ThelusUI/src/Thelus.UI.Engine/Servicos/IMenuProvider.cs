using System.Collections.Generic;
using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Engine.Servicos
{
    public interface IMenuProvider
    {
        Task<List<MenuItem>> ObterMenuItensAsync();
    }
}