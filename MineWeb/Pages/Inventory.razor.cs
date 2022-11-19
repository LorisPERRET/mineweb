using Microsoft.AspNetCore.Components;
using MineWeb.Model;

namespace MineWeb.Pages
{
    public partial class Inventory
    {
        private Item[] items;

        [Inject]
        public HttpClient Http { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            items = await Http.GetFromJsonAsync<Item[]>($"{NavigationManager.BaseUri}fake-data.json");
        }
    }
}
