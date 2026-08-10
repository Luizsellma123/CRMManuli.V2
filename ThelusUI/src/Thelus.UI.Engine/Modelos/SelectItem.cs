namespace Thelus.UI.Engine.Modelos
{
    public class SelectItem
    {
        public object Value { get; set; }
        public string Text { get; set; }

        public SelectItem() { }

        public SelectItem(object value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}