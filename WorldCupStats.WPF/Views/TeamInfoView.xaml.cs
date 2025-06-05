using System.Windows;
using WorldCupStats.Data.Models;

namespace WorldCupStats.WPF.Views
{
    public partial class TeamInfoView : Window
    {
        public TeamInfoView(TeamStatistics statistics)
        {
            InitializeComponent();

            txtTeamName.Text = statistics.Country;
            txtFifaCode.Text = $"FIFA Code: {statistics.FifaCode}";
            txtGamesPlayed.Text = statistics.MatchCount.ToString();
            txtWins.Text = statistics.Wins.ToString();
            txtDraws.Text = statistics.Draws.ToString();
            txtLosses.Text = statistics.Losses.ToString();
            txtGoalsScored.Text = statistics.GoalsScored.ToString();
            txtGoalsConceded.Text = statistics.GoalsConceded.ToString();
            txtGoalDifference.Text = (statistics.GoalDifference == 0 ? "0" 
	            : $"{(statistics.GoalDifference >= 0 ? "+" : "")}{statistics.GoalDifference.ToString()}");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
} 