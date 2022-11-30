using Blazorise.DataGrid;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MineWeb.Model;
using MineWeb.Services;
using static System.Net.WebRequestMethods;

namespace MineWeb.Component
{
    public partial class ItemList
    {
        private List<Item> items;

        private int totalItem;

        private int currentPage = 1;

        private int pageSize = 6;

        private int nbPage;

        private string valueInputSearch {
            get
            {
                return this.valueInputSearch;
            }
            set
            {
                this.valueInputSearch = value;
                /*await ReadDataAll(currentPage);*/
            }
        }

        public Item CurrentDragItem { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        protected override async Task OnInitializedAsync()
        {
            totalItem = await DataService.Count();
            nbPage = totalItem / pageSize;
            await ReadDataAll(1);
        }

        public async Task Button(int action)
        {
            if(action == 1)
            {
                if(currentPage == nbPage)
                {
                    await ReadDataAll(currentPage);
                } else
                {
                    await ReadDataAll(currentPage + 1);
                    currentPage++;
                }
            } else
            {
                if(currentPage == 1)
                {
                    await ReadDataAll(currentPage);
                } else
                {
                    await ReadDataAll(currentPage - 1);
                    currentPage--;
                }
                
            }
        }

        private async Task ReadDataAll(int page)
        {
            if(valueInputSearch == null)
            {
                items = await DataService.List(page, pageSize);
            } else
            {
                items = await DataService.SearchItem(valueInputSearch.ToLower(), totalItem);
            }
        }

        private async Task Search(KeyboardEventArgs e)
        {
            items = await DataService.SearchItem(valueInputSearch.ToLower(), totalItem);
        }
    }
}
