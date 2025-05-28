using WorldCupStats.Data.Models;

public class MatchAttendanceComparer : IComparer<Match>
{
	public int Compare(Match x, Match y)
	{
		if (x == null && y == null) return 0;
		if (x == null) return 1;
		if (y == null) return -1;

		return x.Attendance.CompareTo(y.Attendance);
	}
}