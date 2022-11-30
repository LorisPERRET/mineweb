namespace MineWeb.Model
{
    public class ItemForInventoryArchive
    {
        public int Position { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public ItemForInventoryArchive(int position, Item item, int quantity = 1)
        {
            Position = position;
            Item = item;
            Quantity = quantity;
        }
    }
}
