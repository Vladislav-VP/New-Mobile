using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Configurations;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Resources;

namespace TestProject.Services.Helpers
{
    public class UserDialogsHelper : IDialogsHelper
    {
        public void ToastMessage(string message, int duration = 3000)
        {
            ToastConfig toast = new ToastConfig(message);
            toast.SetDuration(duration);
            toast.SetPosition(ToastPosition.Top);
            UserDialogs.Instance.Toast(toast);
        }

        public async Task<bool> Confirm(string message)
        {
            ConfirmConfig config = new ConfirmConfig();
            config.Message = message;
            config.UseYesNo();
            return await UserDialogs.Instance.ConfirmAsync(config);
        }

        public void AlertMessage(string message)
        {
            AlertConfig alert = new AlertConfig();
            alert.Message = message;
            UserDialogs.Instance.Alert(alert);
        }

        public async Task<EditPhotoDialogResult> ChooseOption()
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
