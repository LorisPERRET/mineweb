using Microsoft.AspNetCore.Components;
using MineWeb.Model;
using System.Collections.Generic;

namespace MineWeb.Component
{
    public partial class ItemComponent
    {

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public ItemForInventory Item { get; set; }

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

            if (this.Item == null)
            {
                this.Item = Parent.CurrentDragItem;
            }
            else if (this.Item.Item.Name == Parent.CurrentDragItem.Item.Name)
            {
                if (this.Item.Quantity < this.Item.Item.StackSize)
                {
                    this.Item.Quantity = this.Item.Quantity + 1;
                }
            }
        }

        private void OnDragStart()
        {
            if (this.Item != null)
            {
                Parent.CurrentDragItem = new ItemForInventory(this.Item.Item, 1);
                if (this.Item.Quantity > 1)
                {
                    this.Item.Quantity = this.Item.Quantity - 1;
                }
                else
                {
                    this.Item = null;
                }
            }
        }

    }
}
