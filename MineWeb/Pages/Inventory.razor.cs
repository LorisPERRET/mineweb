using Microsoft.AspNetCore.Components;
using MineWeb.Model;

namespace MineWeb.Pages
{
    public partial class Inventory
    {
        private Item[] items;

        private Dictionary<int,Item> ItemsInventory = new Dictionary<int, Item>();

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            items = await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json");

            Item[] liste = await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data-inventory.json");
            int cpt = 0;
            foreach (Item i in liste)
            {
                ItemsInventory.Add(cpt, i);
                cpt++;
            }
            for (int i = cpt; i < 27;i++)
            {
                ItemsInventory.Add(i, new Item());
            }
        }
    }
}
