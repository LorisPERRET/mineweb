namespace MineWeb.Model
{
    public static class ItemFactory
    {
        public static ItemModel ToModel(Item item, byte[] imageContent)
        {
            return new ItemModel
            {
                Id = item.Id,
                DisplayName = item.DisplayName,
                Name = item.Name,
                StackSize = item.StackSize,
                ImageContent = imageContent,
                ImageBase64 = string.IsNullOrWhiteSpace(item.ImageBase64) ? Convert.ToBase64String(imageContent) : item.ImageBase64
            };
        }

        public static Item Create(ItemModel model)
        {
            return new Item
            {
                Id = model.Id,
                DisplayName = model.DisplayName,
                Name = model.Name,
                StackSize = model.StackSize,
                ImageBase64 = Convert.ToBase64String(model.ImageContent)
            };
        }

        public static void Update(Item item, ItemModel model)
        {
            item.DisplayName = model.DisplayName;
            item.Name = model.Name;
            item.StackSize = model.StackSize;
            item.ImageBase64 = Convert.ToBase64String(model.ImageContent);
        }
    }
}
