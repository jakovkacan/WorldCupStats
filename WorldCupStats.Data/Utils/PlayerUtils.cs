using WorldCupStats.Data.Models;

namespace WorldCupStats.Data.Utils;

public class PlayerUtils
{
	public static IEnumerable<Player> UpdatePlayerFavoritesPictures(IEnumerable<Player> players,
		List<Player> favoritePlayers, List<PlayerPicture> pictures)
	{
		var updatedPlayers = players.ToList();
		updatedPlayers.ForEach(p =>
		{
			if (p.IsFavorite && favoritePlayers.All(fp => fp.Name != p.Name)) 
				p.IsFavorite = false; // Remove favorite status if not in favorites list

			if (favoritePlayers.Any(fp => fp.Name == p.Name))
				p.IsFavorite = true;
			
			if (pictures.Any(pic => pic.Name == p.Name))
				p.PicturePath = pictures.First(pic => pic.Name == p.Name).PictureFileName;
		});

		return updatedPlayers;
	}
}