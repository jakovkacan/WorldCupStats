using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Interfaces;

public interface IDataRepository
{
	Task<IEnumerable<Team>> GetAllTeamsAsync(ChampionshipType type);
	Task<IEnumerable<Match>> GetAllMatchesAsync(ChampionshipType type);
	Task<IEnumerable<Match>> GetAllMatchesAsync(ChampionshipType type, string fifaCode);
}