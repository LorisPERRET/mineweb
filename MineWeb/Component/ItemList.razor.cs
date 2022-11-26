using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MineWeb.Model;
using MineWeb.Services;

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
                items = await DataService.Search(valueInputSearch.ToLower()));
                /*foreach (var item in items)
                {
                    Console.WriteLine(item.Name);
                    if (item.Name.Contains(valueInputSearch.ToLower()))
                    {
                        Console.WriteLine(item.Name);
                    }
                }*/
                /*items = items.Where(value => items.Any(valueInputSearch => value.Name.Contains(valueInputSearch)));*/
            }
            
        }
    }
}
