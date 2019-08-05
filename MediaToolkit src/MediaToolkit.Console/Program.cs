using System;
using System.IO;
using System.Threading.Tasks;
using MediaToolkit.Model;
using MediaToolkit.Options;
using Microsoft.WindowsAzure.Storage;
using pelazem.azure.storage;

namespace MediaToolkit.Console
{
	class Program
	{
		static async Task Main(string[] args)
		{
			int secondsStep = 10;
			int currentSeek = 0;

			var inputFile = new MediaFile(@"video1.mp4");
			string outputPathRoot = @"video1_";

			var options = new ConversionOptions();

			using (var engine = new Engine())
			{
				engine.GetMetadata(inputFile);

				while (currentSeek <= inputFile.Metadata.Duration.TotalSeconds)
				{
					options.Seek = TimeSpan.FromSeconds(currentSeek);

					string outputFileName = outputPathRoot + string.Format("{0:D4}", currentSeek) + ".jpg";

					string outputFolderPath = Path.GetDirectoryName(outputFileName);

					if (!Directory.Exists(outputFolderPath))
						Directory.CreateDirectory(outputFolderPath);

					var outputFile = new MediaFile(outputFileName);

					//var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(currentSeek) };
					engine.GetThumbnail(inputFile, outputFile, options);

					currentSeek += secondsStep;
				}
			}
		}
	}
}
