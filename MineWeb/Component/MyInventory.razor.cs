using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MineWeb.Component
{
    public partial class MyInventory
    {
        [Parameter]
        public Dictionary<int, MineWeb.Model.ItemForInventory> Items { get; set; }

        public MineWeb.Model.ItemForInventory CurrentDragItem { get; set; }

        public int CurrentDragItemIndex { get; set; }
        public ObservableCollection<MineWeb.Model.InventoryAction> Actions { get; set; }

        public MyInventory()
        {
            Actions = new ObservableCollection<MineWeb.Model.InventoryAction>();
            Actions.CollectionChanged += OnActionsCollectionChanged;
        }

        public void UndoDrag() // Doit modifier le dictionnaire des items et appeler StateHasChanged
        {
            this.Items[this.CurrentDragItemIndex] = CurrentDragItem;
            StateHasChanged();
        } 

        public void ActualizeQuantity(int newQuantity)
        {
            this.Items[this.CurrentDragItemIndex] = CurrentDragItem;
            this.Items[this.CurrentDragItemIndex].Quantity = newQuantity;
            StateHasChanged();
        }


        [Inject]
        internal IJSRuntime JavaScriptRuntime { get; set; }
        private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            JavaScriptRuntime.InvokeVoidAsync("MyInventory.AddActions", e.NewItems);
        }
    }
}
