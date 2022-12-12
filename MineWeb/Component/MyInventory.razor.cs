using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.JSInterop;
using MineWeb.Model;
using MineWeb.Pages;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.Json;

namespace MineWeb.Component
{
    public partial class MyInventory
    {
        [Parameter]
        public Dictionary<int, MineWeb.Model.ItemForInventory> Items { get; set; }

        public MineWeb.Model.ItemForInventory CurrentDragItem { get; set; }

        public int CurrentDragItemIndex { get; set; }

        public ObservableCollection<MineWeb.Model.InventoryAction> Actions { get; set; }

        [Inject]
        public IWebHostEnvironment WebHostEnvironment { get; set; }

        [CascadingParameter]
        public Inventory Parent { get; set; }

        public MyInventory()
        {
            Actions = new ObservableCollection<MineWeb.Model.InventoryAction>();
            Actions.CollectionChanged += OnActionsCollectionChanged;
        }

        public void UndoDrag() // Doit modifier le dictionnaire des items et appeler StateHasChanged
        {
            if(this.CurrentDragItemIndex != -1)
            {
                this.Items[this.CurrentDragItemIndex] = CurrentDragItem;
                StateHasChanged();
            }
        } 

        public void ActualizeQuantity(int newQuantity)
        {
            this.Items[this.CurrentDragItemIndex] = CurrentDragItem;
            this.Items[this.CurrentDragItemIndex].Quantity = newQuantity;
            StateHasChanged();
        }

        public bool FindAPlaceFor(ItemForInventory itemToPlace, int initialIndex)
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i] == null)
                {
                    this.Items[i] = itemToPlace;
                    StateHasChanged();
                    return true;
                }
                else if (this.Items[i].Item.Id == itemToPlace.Item.Id 
                        && this.Items[i].Quantity + itemToPlace.Quantity <= itemToPlace.Item.StackSize
                        && i != initialIndex)
                {
                    this.Items[i].Quantity = this.Items[i].Quantity + itemToPlace.Quantity;
                    StateHasChanged();
                    return true;
                }
            }
            return false;
        }


        [Inject]
        internal IJSRuntime JavaScriptRuntime { get; set; }
        private void OnActionsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            JavaScriptRuntime.InvokeVoidAsync("MyInventory.AddActions", e.NewItems);
        }

        public void saveData()
        {
            ItemForInventoryArchive[] listeASave = new ItemForInventoryArchive[27];
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (Items[i] != null)
                {
                    ItemForInventoryArchive item = new ItemForInventoryArchive(i, Items[i].Item, Items[i].Quantity);
                    listeASave[i] = item;
                }
                
            }
            var jsonString = JsonSerializer.Serialize(listeASave);
            File.WriteAllText($"{WebHostEnvironment.WebRootPath}/save-data-inventory.json", jsonString);
        }
    }
}
