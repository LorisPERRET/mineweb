using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MineWeb.Services;
using MineWeb.Model;

namespace MineWeb.Pages
{
    public partial class Edit
    {
        [Parameter]
        public int Id { get; set; }

        /// <summary>
        /// The current item model
        /// </summary>
        private ItemModel itemModel = new();

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var item = await DataService.GetById(Id);

            var fileContent = await File.ReadAllBytesAsync($"{WebHostEnvironment.WebRootPath}/images/default.png");

            if (File.Exists($"{WebHostEnvironment.WebRootPath}/images/{itemModel.Name}.png"))
            {
                fileContent = await File.ReadAllBytesAsync($"{WebHostEnvironment.WebRootPath}/images/{item.Name}.png");
            }

            // Set the model with the item
            itemModel = ItemFactory.ToModel(item, fileContent);
        }

        private async void HandleValidSubmit()
        {
            await DataService.Update(Id, itemModel);

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
