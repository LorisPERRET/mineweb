using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
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
            if (Parent.Parent.CurrentDragItem != null)
            {
                Parent.CurrentDragItem = new ItemForInventory(Parent.Parent.CurrentDragItem);
                Parent.CurrentDragItemIndex = -1;
                Parent.Parent.CurrentDragItem = null;
            }
            if (Parent.CurrentDragItem != null)
            {
                //Parent.Actions.Add(new InventoryAction { Action = "Drag Enter", Item = Parent.CurrentDragItem.Item, Index = Parent.CurrentDragItemIndex });
            }
            
        }

        internal void OnDragLeave()
        {
            if (NoDrop)
            {
                return;
            }

            if (Parent.CurrentDragItem != null) 
            {
                //Parent.Actions.Add(new InventoryAction { Action = "Drag Leave", Item = Parent.CurrentDragItem.Item, Index = Parent.CurrentDragItemIndex });
            }
        }

        internal void OnDrop()
        {
            if (NoDrop)
            {
                return;
            }
            if (Parent.CurrentDragItem != null) 
            {
                if (this.Item == null)
                {
                    this.Item = Parent.CurrentDragItem;
                    Parent.Items[this.Index] = this.Item;
                }
                else if (this.Item.Item.Name == Parent.CurrentDragItem.Item.Name)
                {
                    int totalQuantity = this.Item.Quantity + Parent.CurrentDragItem.Quantity;
                    if (totalQuantity < this.Item.Item.StackSize)
                    {
                        this.Item.Quantity = totalQuantity;
                        Parent.Items[this.Index] = this.Item;
                    }
                    else // cas où les quantités sont plus importantes
                    {
                        this.Item.Quantity = this.Item.Item.StackSize;
                        Parent.ActualizeQuantity(totalQuantity - this.Item.Item.StackSize);
                    }
                }
                else
                {
                    Parent.UndoDrag();
                }
                Parent.CurrentDragItem = null;
                Parent.Actions.Add(new InventoryAction { Action = "Drag Drop", Item = this.Item.Item, Index = this.Index });
                Parent.saveData();
            }
        }

        private void OnDragStart(MouseEventArgs args)
        {
            if (this.Item != null)
            {
                Parent.Actions.Add(new InventoryAction { Action = "Drag Start", Item = this.Item.Item, Index = this.Index });
                Parent.CurrentDragItem = this.Item;
                Parent.CurrentDragItemIndex = this.Index;
                this.Item = null;
                Parent.Items[Index] = null;
                Parent.saveData();
            }
        }

        private void OnClick(MouseEventArgs eventArgs)
        {
            Console.WriteLine("Left click");
        }

        private void OnRightClick(MouseEventArgs args)
        {
            if ( this.Item != null)
            {
                if (Parent.FindAPlaceFor(new ItemForInventory(this.Item.Item, this.Item.Quantity / 2), this.Index))
                {
                    this.Item.Quantity = this.Item.Quantity - this.Item.Quantity/2;
                    Parent.saveData();
                }
            }
        }
    }
}
