using Microsoft.AspNetCore.Components;

namespace MineWeb.Component
{
    public partial class Item
    {

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public MineWeb.Model.Item CurrentItem { get; set; }

        [Parameter]
        public bool NoDrop { get; set; }

        [CascadingParameter]
        public MyInventory Parent { get; set; }

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
