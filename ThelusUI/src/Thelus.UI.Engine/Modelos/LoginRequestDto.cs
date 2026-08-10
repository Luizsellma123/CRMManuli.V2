namespace Thelus.UI.Engine.Modelos
{
    public class LoginRequestDto
    {
        public string Usuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public int EmpresaId { get; set; } = 1;
    }
}