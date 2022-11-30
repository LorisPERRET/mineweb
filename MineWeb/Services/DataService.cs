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

        public async Task<List<Item>> List(int currentPage, int pageSize)
        {
            return await _http.GetFromJsonAsync<List<Item>>($"https://localhost:7234/api/Crafting/?currentPage={currentPage}&pageSize={pageSize}");
        }

        public async Task<Item> GetById(int id)
        {
            return await _http.GetFromJsonAsync<Item>($"https://localhost:7234/api/Crafting/{id}");
        }
        public async Task<List<Item>> SearchItem(string valueInput, int totalItem)
        {
            List<Item> itemsSearch = await List(1, totalItem);
            List<Item> items = new List<Item>();
            foreach (var item in itemsSearch)
            {
                if (item.Name.Contains(valueInput))
                {
                    items.Add(item);
                }
            }
            return items;
        }
    }
}
