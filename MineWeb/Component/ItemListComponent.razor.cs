using Microsoft.AspNetCore.Components;
using MineWeb.Model;

namespace MineWeb.Component
{
    public partial class ItemListComponent
    {
        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public Item CurrentItem { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

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
            Parent.CurrentDragItem = this.CurrentItem;
        }
    }
}
