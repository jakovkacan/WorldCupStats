using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Interfaces;

public interface ITeamRepository
{
	Task<IEnumerable<Team>> GetAllTeamsAsync();
	Task<Team> GetTeamByIdAsync(int id);
	Task<Team> GetTeamByFifaCode(string fifaCode);
}