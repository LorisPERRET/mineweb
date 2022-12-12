using Microsoft.AspNetCore.Hosting;
using MineWeb.Model;

namespace MineWeb.Services
{
    public class DataService : IDataService
    {
        private readonly HttpClient _http;

        public DataService(
            HttpClient http)
        {
            _http = http;
        }

        public async Task<int> Count()
        {
            return await _http.GetFromJsonAsync<int>("https://localhost:7234/api/Crafting/count");
        }

        public async Task<List<Item>> List(int currentPage, int pageSize, bool sorted, int totalItem)
        {
            List<Item> itemsTmp = new List<Item>();
            List<Item> items = new List<Item>();
            int nbMin = (currentPage - 1) * pageSize;
            int nbMax = nbMin + pageSize;
            if (sorted)
            {
                itemsTmp = await _http.GetFromJsonAsync<List<Item>>($"https://localhost:7234/api/Crafting/?currentPage={1}&pageSize={totalItem}");
                itemsTmp = itemsTmp.OrderBy(o => o.DisplayName).ToList();
                for (int i = nbMin; i < nbMax; i++)
                {
                    items.Add(itemsTmp[i]);
                }
            }
            else
            {
                items = await _http.GetFromJsonAsync<List<Item>>($"https://localhost:7234/api/Crafting/?currentPage={currentPage}&pageSize={pageSize}");
            }

            return items;
        }

        public async Task<Item> GetById(int id)
        {
            return await _http.GetFromJsonAsync<Item>($"https://localhost:7234/api/Crafting/{id}");
        }

        public async Task<(List<Item>, int)> SearchItem(int currentPage, int pageSize, bool sorted, string valueInput, int totalItem)
        {
            List<Item> itemsSearch = await List(1, totalItem, sorted, totalItem);
            List<Item> itemsTmp = new List<Item>();
            List<Item> items = new List<Item>();
            int nbMin = (currentPage - 1) * pageSize;
            int nbMax = nbMin + pageSize;

            foreach (var item in itemsSearch)
            {
                if (item.DisplayName.Contains(valueInput) || item.Name.Contains(valueInput))
                {
                    itemsTmp.Add(item);
                }
            }
            for (int i = nbMin; i < nbMax; i++)
            {
                if (itemsTmp.Count > i)
                {
                    items.Add(itemsTmp[i]);
                }
            }
            if (sorted)
            {
                items = items.OrderBy(o => o.DisplayName).ToList();
            }
            return (items: items, nbSearch: itemsTmp.Count);
        }

        public async Task Add(ItemModel model)
        {
            // Get the item
            var item = ItemFactory.Create(model);

            // Save the data
            await _http.PostAsJsonAsync("https://localhost:7234/api/Crafting/", item);
        }

        public async Task Update(int id, ItemModel model)
        {
            // Get the item
            var item = ItemFactory.Create(model);

            await _http.PutAsJsonAsync($"https://localhost:7234/api/Crafting/{id}", item);
        }

        public async Task Delete(int id)
        {
            await _http.DeleteAsync($"https://localhost:7234/api/Crafting/{id}");
        }
    }
}
