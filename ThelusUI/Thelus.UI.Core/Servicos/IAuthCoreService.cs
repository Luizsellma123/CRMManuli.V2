using System.Threading.Tasks;
using Thelus.UI.Engine.Modelos;

namespace Thelus.Core.Servicos
{
    public interface IAuthCoreService
    {
        Task<LoginResponseDto> AutenticarAsync(LoginRequestDto request);
    }
}