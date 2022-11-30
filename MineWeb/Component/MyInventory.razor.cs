using Microsoft.AspNetCore.Components;


namespace MineWeb.Component
{
    public partial class MyInventory
    {
        [Parameter]
        public Dictionary<int, MineWeb.Model.ItemForInventory> Items { get; set; }

        public MineWeb.Model.ItemForInventory CurrentDragItem { get; set; }

        public MineWeb.Model.ItemForInventory CurrentDragItemIsFrom { get; set; }
    }
}
