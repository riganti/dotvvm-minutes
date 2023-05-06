using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;
using ImageResizerDemo.Services;
using ImageResizerDemo.Services.BackgroundOperations;

namespace ImageResizerDemo.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        private readonly IBackgroundOperationFactory backgroundOperationFactory;
        private readonly ImageResizingService imageResizingService;


        public int CurrentStep { get; set; } = 0;

        public UploadedFilesCollection Images { get; set; } = new();

        public List<int> RequestedHeights { get; set; } = new();
        
        public int[] AvailableHeights => new[] { 100, 200, 300, 400, 600, 800, 1000, 1200 };


        public BackgroundOperationProgress Progress { get; set; }

        public string ResultArchiveUrl { get; set; }


        public DefaultViewModel(IBackgroundOperationFactory backgroundOperationFactory, ImageResizingService imageResizingService)
        {
            this.backgroundOperationFactory = backgroundOperationFactory;
            this.imageResizingService = imageResizingService;
        }


        public void StartProcessing()
        {
            CurrentStep = 1;

            Progress = backgroundOperationFactory.Start(operationContext =>
            {
                return imageResizingService.ResizeImagesAsync(Images.Files, RequestedHeights, operationContext);
            });

            ResultArchiveUrl = "/output/" + Progress.OperationId + ".zip";
        }

    }
}
