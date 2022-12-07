using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MineWeb.Model;
using System.Collections.Generic;

namespace MineWeb.Pages
{
    public partial class Inventory
    {
        private Item[] items;

        private Dictionary<int,ItemForInventory> ItemsInventory = new Dictionary<int, ItemForInventory>();

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<Inventory> Localizer { get; set; }

        protected override async Task OnInitializedAsync()
        {
            items = await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json");

            ItemForInventoryArchive[] liste = await Http.GetFromJsonAsync<ItemForInventoryArchive[]>($"{NavigationManager.BaseUri}fake-data-inventory.json");
            foreach (ItemForInventoryArchive i in liste)
            {
                ItemsInventory.Add(i.Position, new ItemForInventory(i.Item, i.Quantity));
            }
            for (int i = 0; i < 27; i++)
            {
                if (!ItemsInventory.ContainsKey(i))
                {
                    ItemsInventory.Add(i, null);
                }
            }
            Console.WriteLine(ItemsInventory);
        }
    }
}
