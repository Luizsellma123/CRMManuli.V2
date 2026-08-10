using System.Collections.Generic;

namespace Thelus.UI.Engine.Modelos
{
    public class LoginResponseDto
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string NomeUsuario { get; set; } = string.Empty;
        public List<int> IdsMenuPermitidos { get; set; } = new();
    }
}