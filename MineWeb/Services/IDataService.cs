using MineWeb.Model;

namespace MineWeb.Services
{
    public interface IDataService
    {
        Task<int> Count();

        Task<List<Item>> List(int currentPage, int pageSize, bool sorted, int totalItem);

        Task<Item> GetById(int id);

        Task<(List<Item>, int)> SearchItem(int page, int pageSize, bool sorted, string v, int totalItem);
    }
}
