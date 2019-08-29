using System.Threading.Tasks;

using Acr.UserDialogs;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class DialogsHelper : IDialogsHelper
    {
        public void DisplayToastMessage(string message, int duration = 3000)
        {
            ToastConfig toast = new ToastConfig(message);
            toast.SetDuration(duration);
            toast.SetPosition(ToastPosition.Top);
            UserDialogs.Instance.Toast(toast);
        }

        public async Task<bool> IsConfirmed(string message)
        {
            ConfirmConfig config = new ConfirmConfig();
            config.Message = message;
            config.UseYesNo();
            return await UserDialogs.Instance.ConfirmAsync(config);
        }

        public void DisplayAlertMessage(string message)
        {
            AlertConfig alert = new AlertConfig();
            alert.Message = message;
            UserDialogs.Instance.Alert(alert);
        }

        public async Task<string> ChooseOption(string title, string cancel, string destructive, params string[] buttons)
        {
            string option = await UserDialogs
                .Instance
                .ActionSheetAsync(title, cancel, destructive, buttons: buttons);

            return option;
        }
    }
}
