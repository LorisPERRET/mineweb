namespace MineWeb.Model
{
    public class ItemForInventory
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }

        public ItemForInventory(Item item, int quantity = 1)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}
