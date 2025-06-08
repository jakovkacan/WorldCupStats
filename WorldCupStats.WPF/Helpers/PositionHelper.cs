using System.Windows;
using WorldCupStats.Data.Models;

namespace WorldCupStats.WPF.Helpers
{
    public static class PositionHelper
    {
        public static Point GetPositionOnField(Position position, int playerIndex, int totalPlayersInPosition = 0)
        {
            switch (position)
            {
                case Position.Goalie:
                    return new Point(0.05, 0.5); // Always centered

                case Position.Defender:
                    return GetVerticallySpacedPosition(0.15, playerIndex, totalPlayersInPosition);

                case Position.Midfield:
                    return GetVerticallySpacedPosition(0.3, playerIndex, totalPlayersInPosition);

                case Position.Forward:
                    return GetVerticallySpacedPosition(0.45, playerIndex, totalPlayersInPosition);

                default:
                    return new Point(0.25, 0.5);
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