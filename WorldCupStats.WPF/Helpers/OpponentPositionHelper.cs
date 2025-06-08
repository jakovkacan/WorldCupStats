using System.Windows;
using WorldCupStats.Data.Models;

namespace WorldCupStats.WPF.Helpers
{
    public static class OpponentPositionHelper
    {
        public static Point GetPositionOnField(Position position, int playerIndex, int totalPlayersInPosition = 0)
        {
            switch (position)
            {
                case Position.Goalie:
                    return new Point(0.95, 0.5); // Always centered

                case Position.Defender:
                    return GetVerticallySpacedPosition(0.85, playerIndex, totalPlayersInPosition);

                case Position.Midfield:
                    return GetVerticallySpacedPosition(0.7, playerIndex, totalPlayersInPosition);

                case Position.Forward:
                    return GetVerticallySpacedPosition(0.55, playerIndex, totalPlayersInPosition);

                default:
                    return new Point(0.75, 0.5);
            }
        }

        private static Point GetVerticallySpacedPosition(double xPosition, int playerIndex, int totalPlayers)
        {
            // If there's only one player or no total count provided, center them
            if (totalPlayers <= 1)
            {
                return new Point(xPosition, 0.5);
            }

            // For multiple players, space them evenly
            double spacing = 0.7 / (totalPlayers - 1); // 70% of field height for spacing
            double startY = 0.15; // Start 15% from top
            double yPosition = startY + (spacing * playerIndex);

            return new Point(xPosition, yPosition);
        }
    }
} 