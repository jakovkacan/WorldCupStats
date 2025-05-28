using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Interfaces;

public interface IDataRepository
{
	Task<IEnumerable<Team>> GetAllTeamsAsync();
	Task<IEnumerable<Match>> GetAllMatchesAsync();
	Task<IEnumerable<Match>> GetAllMatchesAsync(string fifaCode);
}