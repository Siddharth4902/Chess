using WebApplication1.Models;

namespace WebApplication1.DAO
{
    public interface IChessDao
    {
        Task<int> InsertMatch(Chess c);

        Task<List<Chess>> GetMatches();

        Task<List<Country>> GetPlayerByCountry(string country);
        Task<List<Performance>> GetPerformances();

        Task<List<Performance>> GetWinners();
    }
}
