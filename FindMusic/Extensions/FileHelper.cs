namespace FindMusic.Extensions
{
	public static class FileHelper
	{
		public static string ProcessUploadedFile(this IFormFile photoFile, IWebHostEnvironment hostEnvironment)
		{
			var uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
			var uniqueFileName = Guid.NewGuid().ToString() + '_' + photoFile.FileName;
			var filePath = Path.Combine(uploadsFolder, uniqueFileName);

			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				photoFile.CopyTo(fileStream);
			}

			return uniqueFileName;
		}

		public static void DeleteImageFile(this string filePath, IWebHostEnvironment hostEnvironment)
		{
			filePath = Path.Combine(hostEnvironment.WebRootPath, "images", filePath);

			try
			{
				GC.Collect();
				GC.WaitForPendingFinalizers();
				File.Delete(filePath);
			}
			catch (Exception e)
			{
				// ignored
			}
		}
	}
}
