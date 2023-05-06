using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Core.Storage;
using ImageResizerDemo.Services.BackgroundOperations;
using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;

namespace ImageResizerDemo.Services
{
    public class ImageResizingService
    {
        private readonly IUploadedFileStorage uploadedFileStorage;
        private readonly string outputFolder;

        public ImageResizingService(IUploadedFileStorage uploadedFileStorage, IWebHostEnvironment env)
        {
            this.uploadedFileStorage = uploadedFileStorage;

            this.outputFolder = Path.Combine(env.WebRootPath, "output");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
        }

        public async Task ResizeImagesAsync(ICollection<UploadedFile> files, ICollection<int> heights, IBackgroundOperationContext operationContext)
        {
            await operationContext.ReportProgress(0, files.Count, "Starting operation...");

            var objectsToDispose = new List<IDisposable>();

            try
            {
                // gather all files to process
                var filesToProcess = await GatherAllFiles(files, operationContext, objectsToDispose);

                // prepare output archive
                using var outputStream = File.OpenWrite(Path.Combine(outputFolder, operationContext.OperationId + ".zip"));
                using var outputArchive = new ZipArchive(outputStream, ZipArchiveMode.Create);

                // process images
                var index = 0;
                foreach (var file in filesToProcess)
                {
                    index++;
                    await operationContext.ReportProgress(index, filesToProcess.Count, $"Processing image {file.FileName}...");

                    try
                    {
                        await ProcessFile(file, heights, outputArchive, $"image{index:000}", operationContext);
                    }
                    catch (Exception ex)
                    {
                        file.IsError = true;
                    }
                }

                if (filesToProcess.Any(f => f.IsError))
                {
                    await operationContext.ReportError("The following files couldn't be processed: " + string.Join(", ", filesToProcess.Where(f => f.IsError).Select(f => f.FileName)));
                }
                else
                {
                    await operationContext.ReportSuccess("All images processed.");
                }
            }
            finally
            {
                foreach (var obj in objectsToDispose)
                {
                    obj.Dispose();
                }
            }
        }

        private async Task<List<FileToProcess>> GatherAllFiles(ICollection<UploadedFile> files, IBackgroundOperationContext operationContext, List<IDisposable> objectsToDispose)
        {
            var filesToProcess = new List<FileToProcess>();

            foreach (var file in files)
            {
                if (file.FileName.EndsWith(".zip", StringComparison.OrdinalIgnoreCase))
                {
                    // file is archive
                    await operationContext.ReportProgress(0, files.Count, $"Unzipping archive {file.FileName}...");

                    var stream = await uploadedFileStorage.GetFileAsync(file.FileId);
                    objectsToDispose.Add(stream);
                    var archive = new ZipArchive(stream, ZipArchiveMode.Read);
                    objectsToDispose.Add(archive);

                    foreach (var entry in archive.Entries)
                    {
                        filesToProcess.Add(new FileToProcess()
                        {
                            FileName = file.FileName + ":" + entry.FullName,
                            GetStream = () => Task.FromResult(entry.Open())
                        });
                    }
                }
                else
                {
                    // file is pure image
                    filesToProcess.Add(new FileToProcess()
                    {
                        FileName = file.FileName,
                        GetStream = () => uploadedFileStorage.GetFileAsync(file.FileId)
                    });
                }
            }

            return filesToProcess;
        }

        private async Task ProcessFile(FileToProcess file, ICollection<int> heights, ZipArchive outputArchive, string namePrefix, IBackgroundOperationContext backgroundOperationContext)
        {
            using var image = await Image.LoadAsync(await file.GetStream());

            foreach (var height in heights)
            {
                using var resizedImage = image.Clone(x => x.Resize(0, height));

                var entry = outputArchive.CreateEntry($"{namePrefix}_{height}px.jpg");
                await using var entryStream = entry.Open();
                await resizedImage.SaveAsync(entryStream, new JpegEncoder() { Quality = 95 });
            }
        }
    }

    public class FileToProcess
    {
        public string FileName { get; set; }

        public Func<Task<Stream>> GetStream { get; set; }

        public bool IsError { get; set; }
    }
}
