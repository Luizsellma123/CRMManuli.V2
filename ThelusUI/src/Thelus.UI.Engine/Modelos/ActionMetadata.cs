using Thelus.UI.Engine.Atributos;

namespace Thelus.UI.Engine.Modelos
{
    public class ActionMetadata
    {
        public string Label { get; set; }
        public string Icon { get; set; }
        public string CssClass { get; set; }
        public ActionType ActionType { get; set; }
        public string TargetUrl { get; set; }
        public int Order { get; set; }
        public string Section { get; set; }
    }
}