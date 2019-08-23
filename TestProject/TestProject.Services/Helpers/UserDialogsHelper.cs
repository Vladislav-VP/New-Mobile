using System.Collections.Generic;
using System.Threading.Tasks;

using Acr.UserDialogs;

using TestProject.Resources;
using TestProject.Services.Enums;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class UserDialogsHelper : DialogsHelper, IUserDialogsHelper
    {
        public async Task<EditPhotoDialogResult> ChoosePhotoEditOption()
        {
            string[] buttons =
                {
                    Strings.ChoosePicture,
                    Strings.TakePicture
                };

            Dictionary<string, EditPhotoDialogResult> keyValuePairs = new Dictionary<string, EditPhotoDialogResult>();
            keyValuePairs.Add(Strings.CancelText, EditPhotoDialogResult.Cancel);
            keyValuePairs.Add(Strings.ChoosePicture, EditPhotoDialogResult.ChooseFromGallery);
            keyValuePairs.Add(Strings.TakePicture, EditPhotoDialogResult.TakePicture);
            keyValuePairs.Add(Strings.DeletePicture, EditPhotoDialogResult.DeletePicture);

            var result = await UserDialogs.Instance.ActionSheetAsync(Strings.ProfilePhotoTitle,
                Strings.CancelText, Strings.DeletePicture, buttons: buttons);

            return keyValuePairs[result];
        }
    }
}
