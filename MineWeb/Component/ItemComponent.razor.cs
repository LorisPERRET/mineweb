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
        public Item Item { get; set; }

        [Parameter]
        public bool NoDrop { get; set; }

        [Parameter]
        public int  Quantity { get; set; }

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

            Parent.Items[Index] = new Item();
        }

        internal void OnDrop()
        {
            if (NoDrop)
            {
                return;
            }

            if (this.Quantity == 0)
            {
                Item = Parent.CurrentDragItem;
                Parent.Items[Index] = Parent.CurrentDragItem;
            }
            else if (this.Item == Parent.CurrentDragItem)
            {
                if (this.Quantity < this.Item.StackSize)
                {
                    this.Quantity += 1;
                }
            }
        }

        private void OnDragStart()
        {
            if (this.Quantity > 0)
            {
                Parent.CurrentDragItem = this.Item;
                if (this.Quantity == 1)
                {
                    this.Item = new Item();
                    Parent.Items[Index] = this.Item;
                }
                else
                {
                    this.Quantity -= 1;
                }
            }
        }

    }
}
