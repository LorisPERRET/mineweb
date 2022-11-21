using MineWeb.Model;

namespace MineWeb.Services
{
    public interface IDataService
    {
        Task<int> Count();

        Task<List<Item>> List(int currentPage, int pageSize);

        Task<Item> GetById(int id);
    }
}
