using Microsoft.AspNetCore.Components;


namespace MineWeb.Component
{
    public partial class MyInventory
    {
        [Parameter]
        public Dictionary<int, MineWeb.Model.Item> Items { get; set; }

        public MineWeb.Model.Item CurrentDragItem { get; set; }
    }
}
