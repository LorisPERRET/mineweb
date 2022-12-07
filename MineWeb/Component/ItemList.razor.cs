using Blazorise.Extensions;
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

        private int currentPage = 1;

        private int pageSize = 6;

        private int nbPage;

        private string valueInputSearch;
        public string ValueIntputSearch
        {
            get
            {
                return valueInputSearch;
            }
            set
            {
                valueInputSearch = value;
                this.InvokeAsync(async () => await ReadDataAll(currentPage));
            }
        }

        public Item CurrentDragItem { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public IStringLocalizer<ItemList> Localizer { get; set; }

        private async Task OnReadData(DataGridReadDataEventArgs<Item> e)
        {
            this.ValueIntputSearch = string.Empty;
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
            if(this.ValueIntputSearch.IsNullOrEmpty())
            {
                items = await DataService.List(page, pageSize);
                nbPage = totalItem / pageSize;
            }
            else
            {
                var result = await DataService.SearchItem(page, pageSize, valueInputSearch.ToLower(), totalItem);
                int nbItemSearch = result.Item2;
                items = result.Item1;
                nbPage = nbItemSearch / pageSize;
            }
            StateHasChanged();
        }
    }
}
