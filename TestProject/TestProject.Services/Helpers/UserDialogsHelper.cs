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

            Dictionary<string, EditPhotoDialogResult> messageOptionPairs = 
                new Dictionary<string, EditPhotoDialogResult>();
            messageOptionPairs.Add(Strings.CancelText, EditPhotoDialogResult.Cancel);
            messageOptionPairs.Add(Strings.ChoosePicture, EditPhotoDialogResult.ChooseFromGallery);
            messageOptionPairs.Add(Strings.TakePicture, EditPhotoDialogResult.TakePicture);
            messageOptionPairs.Add(Strings.DeletePicture, EditPhotoDialogResult.DeletePicture);

            string result = await UserDialogs.Instance.ActionSheetAsync(Strings.ProfilePhotoTitle,
                Strings.CancelText, Strings.DeletePicture, buttons: buttons);

            return messageOptionPairs[result];
        }
    }
}
