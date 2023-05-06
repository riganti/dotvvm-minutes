using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessPackControls.Model;
using DotVVM.Core.Storage;
using DotVVM.Framework.Controls;
using DotVVM.Framework.ViewModel;

namespace BusinessPackControls.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        private readonly IUploadedFileStorage uploadedFileStorage;


        public ProfileModel Profile { get; set; } = new ()
        {
            FullName = "John Doe",
            CountryId = 1
        };

        public UploadedFilesCollection ProfileImage { get; set; } = new ();

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<CountryModel> Countries => CountryModel.AllCountries;

        [Bind(Direction.ServerToClientFirstRequest)]
        public List<InterestModel> Interests => InterestModel.AllInterests;

        public DefaultViewModel(IUploadedFileStorage uploadedFileStorage)
        {
            this.uploadedFileStorage = uploadedFileStorage;
        }

        public async Task ProfileImageUploaded()
        {
            if (ProfileImage.Files.Any())
            {
                var fileId = ProfileImage.Files.First().FileId;
                var filePath = Path.Combine(Context.Configuration.ApplicationPhysicalPath, $"wwwroot/{fileId}.jpg");
                await uploadedFileStorage.SaveAsAsync(fileId, filePath);
                ProfileImage.Clear();

                Profile.ProfileImageUrl = $"{fileId}.jpg";
            }
        }

        public void Save()
        {

        }

        public void Cancel()
        {
            Profile = new()
            {
                FullName = "John Doe",
                CountryId = 1
            };
        }
    }
}
