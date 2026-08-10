using System.Collections.Generic;
using Thelus.UI.Engine.Modelos;

namespace Thelus.UI.Engine.Servicos
{
    public interface IMenuProvider
    {
        List<MenuItem> ObterMenuItens();
    }
}