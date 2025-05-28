namespace WorldCupStats.Data.Utils;

public static class FileUtils
{
	public static string GetBaseDirectory() =>
		Path.Join(
			Directory
				.GetParent(Directory
					.GetParent(Directory
						.GetParent(Directory.GetParent(Directory.GetParent(AppContext.BaseDirectory).FullName).FullName)
						.FullName).FullName).FullName, @"WorldCupStats.Data\LocalData\");

}