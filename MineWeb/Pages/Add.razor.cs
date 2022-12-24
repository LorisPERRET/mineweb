using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MineWeb.Model;
using MineWeb.Services;
using Microsoft.AspNetCore.Hosting;

namespace MineWeb.Pages
{
    public partial class Add
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        /// <summary>
        /// The current item model
        /// </summary>
        private ItemModel itemModel = new();

        private async Task HandleValidSubmit()
        {
            var totalItem = await DataService.Count();
            // Get the current data
            var currentData = await DataService.List(1, totalItem, false, totalItem);

            // Simulate the Id
            itemModel.Id = currentData.Max(s => s.Id) + 1;

            await DataService.Add(itemModel);

            NavigationManager.NavigateTo("inventory");
        }

        private async Task LoadImage(InputFileChangeEventArgs e)
        {
            // Set the content of the image to the model
            using (var memoryStream = new MemoryStream())
            {
                await e.File.OpenReadStream().CopyToAsync(memoryStream);
                itemModel.ImageContent = memoryStream.ToArray();
            }
        }
    }
}
