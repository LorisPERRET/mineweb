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

        public Item CurrentDragItem { get; set; }

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IStringLocalizer<Inventory> Localizer { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //items = await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json");

            int inventorySize = Configuration.GetSection("InventorySize").Get<int>();
            Console.WriteLine(inventorySize);

            ItemForInventoryArchive[] liste = await Http.GetFromJsonAsync<ItemForInventoryArchive[]>($"{NavigationManager.BaseUri}save-data-inventory.json");
            
            for (int i = 0; i < inventorySize; i++)
            {
                if (i >= liste.Count() || liste[i] == null)
                {
                    ItemsInventory.Add(i, null);
                }
                else
                {
                    ItemsInventory.Add(i, new ItemForInventory(liste[i].Item, liste[i].Quantity));
                }
            }
        }
    }
}
