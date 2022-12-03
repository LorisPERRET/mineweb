using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using MineWeb.Model;
using MineWeb.Services;
using System.Collections.Generic;

namespace MineWeb.Component
{
    public partial class ItemList
    {
        private List<Item> items;

        private int totalItem;

        private string valueInputSearch;

        public Item CurrentDragItem { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public IStringLocalizer<ItemList> Localizer { get; set; }

        private async Task OnReadData(DataGridReadDataEventArgs<Item> e)
        {
            if (e.CancellationToken.IsCancellationRequested)
            {
                return;
            }

            if (!e.CancellationToken.IsCancellationRequested)
            {
                items = await DataService.List(e.Page, e.PageSize);
                totalItem = await DataService.Count();
            }
        }

        private async Task Search(KeyboardEventArgs e)
        {
            if (e.Key.Equals("Enter"))
            {
                Console.WriteLine(valueInputSearch);
            }
            /*items.Clear();
            items = await DataService.Search);*/
        }
    }
}
