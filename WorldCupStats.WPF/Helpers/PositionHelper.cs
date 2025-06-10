using System.Windows;
using WorldCupStats.Data.Models;

namespace WorldCupStats.WPF.Helpers
{
	/// <summary>
	/// Provides utility methods for calculating player positions on a field based on their role, team, and index.
	/// </summary>
	/// <remarks>This class is designed to assist in determining the spatial placement of players on a field.
	/// Positions are calculated based on predefined horizontal coordinates for home and away teams,  and vertical spacing
	/// is applied to distribute players evenly within their assigned role.</remarks>
	public static class PositionHelper
	{
		private static class HomePositions
		{
			public const double Goalie = 0.05;
			public const double Defender = 0.15;
			public const double Midfield = 0.30;
			public const double Forward = 0.45;
			public const double Default = 0.25;
		}

		private static class AwayPositions
		{
			public const double Goalie = 0.95;
			public const double Defender = 0.85;
			public const double Midfield = 0.70;
			public const double Forward = 0.55;
			public const double Default = 0.75;
		}

		public static Point GetPositionOnField(Position position, int playerIndex, int totalPlayersInPosition = 0, bool isOpponent = false)
		{
			var xPosition = GetXPosition(position, isOpponent);
			return GetVerticallySpacedPosition(xPosition, playerIndex, totalPlayersInPosition);
		}

		private static double GetXPosition(Position position, bool isOpponent)
		{
			if (isOpponent)
			{
				return position switch
				{
					Position.Goalie => AwayPositions.Goalie,
					Position.Defender => AwayPositions.Defender,
					Position.Midfield => AwayPositions.Midfield,
					Position.Forward => AwayPositions.Forward,
					_ => AwayPositions.Default
				};
			}

			return position switch
			{
				Position.Goalie => HomePositions.Goalie,
				Position.Defender => HomePositions.Defender,
				Position.Midfield => HomePositions.Midfield,
				Position.Forward => HomePositions.Forward,
				_ => HomePositions.Default
			};
		}

		private static Point GetVerticallySpacedPosition(double xPosition, int playerIndex, int totalPlayers)
		{
			// If there's only one player or no total count provided, center them
			if (totalPlayers <= 1)
				return new Point(xPosition, 0.5);
			
			// For multiple players, space them evenly
			var spacing = 0.7 / (totalPlayers - 1); // 70% of field height for spacing
			const double startY = 0.15; // Start 15% from top
			var yPosition = startY + (spacing * playerIndex);

			return new Point(xPosition, yPosition);
		}
	}
}