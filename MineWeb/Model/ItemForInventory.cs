namespace MineWeb.Model
{
    public class ItemForInventory
    {
        public int Position { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public ItemForInventory(int position, Item item, int quantity = 1)
        {
            Position = position;
            Item = item;
            Quantity = quantity;
        }
    }
}
