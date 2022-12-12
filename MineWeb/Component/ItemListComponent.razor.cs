using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using MineWeb.Modals;
using MineWeb.Model;
using MineWeb.Services;

namespace MineWeb.Component
{
    public partial class ItemListComponent
    {
        [Parameter]
        public Item CurrentItem { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [CascadingParameter]
        public IModalService Modal { get; set; }

        [Parameter]
        public bool NoDrop { get; set; }

        [CascadingParameter]
        public ItemList Parent { get; set; }

        internal void OnDragEnter()
        {
            if (NoDrop)
            {
                return;
            }
        }

        internal void OnDragLeave()
        {
            if (NoDrop)
            {
                return;
            }
        }

        internal void OnDrop()
        {
            if (NoDrop)
            {
                return;
            }
        }

        private void OnDragStart()
        {
            Parent.Parent.CurrentDragItem = this.CurrentItem;
        }

        private async void OnDelete(int id)
        {
            var parameters = new ModalParameters();
            parameters.Add(nameof(Item.Id), id);

            var modal = Modal.Show<DeleteConfirmation>("Delete Confirmation", parameters);
            var result = await modal.Result;

            if (result.Cancelled)
            {
                return;
            }

            await DataService.Delete(id);

            // Reload the page
            NavigationManager.NavigateTo("inventory", true);
        }
    }
}
