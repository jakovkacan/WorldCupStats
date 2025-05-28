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

	internal static string CopyPicture(string path)
	{
		if (string.IsNullOrEmpty(path) || !File.Exists(path))
			throw new FileNotFoundException("The specified picture file does not exist.", path);

		var fileName = Path.GetFileName(path);
		var extension = Path.GetExtension(fileName)?.ToLowerInvariant();

		if (string.IsNullOrEmpty(extension) || !IsValidImageExtension(extension))
			throw new InvalidOperationException("The specified file is not a valid image format.");

		var guid = Guid.NewGuid().ToString("N"); // Generate a unique identifier
		var destinationPath = $@"{GetBaseDirectory()}img\{guid}{extension}";

		File.Copy(path, destinationPath);

		return $"{guid}{extension}";
	}

	internal static void DeletePicture(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
			throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

		var filePath = Path.Combine(GetBaseDirectory(), "img", fileName);
		if (File.Exists(filePath))
			File.Delete(filePath);
		else
			throw new FileNotFoundException("The specified picture file does not exist.", filePath);
	}

	public static string GetPicturePath(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
			throw new ArgumentException("File name cannot be null or empty.", nameof(fileName));

		var filePath = Path.Combine(GetBaseDirectory(), "img", fileName);

		if (!File.Exists(filePath))
			throw new FileNotFoundException("The specified picture file does not exist.", filePath);
		
		return filePath;
	}

	private static bool IsValidImageExtension(string extension)
	{
		var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
		return validExtensions.Contains(extension.ToLowerInvariant());
	}
}