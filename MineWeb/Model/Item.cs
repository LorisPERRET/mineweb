namespace MineWeb.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public int StackSize { get; set; }
        public string ImageBase64 { get; set; }
    }
}
