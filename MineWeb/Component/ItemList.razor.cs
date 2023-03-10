using Blazorise.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using MineWeb.Model;
using MineWeb.Pages;
using MineWeb.Services;
using System.Collections.Generic;

namespace MineWeb.Component
{
    public partial class ItemList
    {
        public Item CurrentDragItem { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public IStringLocalizer<ItemList> Localizer { get; set; }

        [Inject]
        public IConfiguration Configuration { get; set; }

        [CascadingParameter]
        public Inventory Parent { get; set; }

        private List<Item> items;

        private int totalItem;

        private int currentPage = 1;

        private bool isSorted;
        public bool IsSorted
        {
            get
            {
                return isSorted;
            }
            set
            {
                isSorted = value;
                this.InvokeAsync(async () => await ReadDataAll(currentPage, isSorted));
            }
        }

        protected override async Task OnInitializedAsync()
        {
            pageSize = Configuration.GetSection("NombreItemPage").Get<int>();
            pageItem = pageSize;
            this.ValueIntputSearch = string.Empty;
            totalItem = await DataService.Count();
            nbPage = totalItem / pageSize;
            await ReadDataAll(1, isSorted);
        }

        private int pageSize;

        private int pageItem;

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
                currentPage = 1;
                this.InvokeAsync(async () => await ReadDataAll(currentPage, isSorted));
            }
        }

        public async Task Button(int action)
        {
            if(action == 1)
            {
                if(currentPage == nbPage)
                {
                    await ReadDataAll(currentPage, isSorted);
                } else
                {
                    await ReadDataAll(currentPage + 1, isSorted);
                    currentPage++;
                }
            } else
            {
                if(currentPage == 1)
                {
                    await ReadDataAll(currentPage, isSorted);
                } else
                {
                    await ReadDataAll(currentPage - 1, isSorted);
                    currentPage--;
                }
            }
        }

        private async Task ReadDataAll(int page, bool sorted)
        {
            if(this.ValueIntputSearch.IsNullOrEmpty())
            {
                items = await DataService.List(page, pageSize, sorted, totalItem);
                nbPage = totalItem / pageSize;
            }
            else
            {
                var result = await DataService.SearchItem(page, pageSize, sorted, valueInputSearch.ToLower(), totalItem);
                int nbItemSearch = result.Item2;
                pageItem = pageSize;
                if (nbItemSearch < pageSize)
                {
                    pageItem = nbItemSearch;
                    nbItemSearch = pageSize;
                }
                items = result.Item1;
                nbPage = nbItemSearch / pageSize;
            }
            StateHasChanged();
        }
    }
}
