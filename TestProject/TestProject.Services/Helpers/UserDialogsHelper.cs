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

        public async Task<bool> ConfirmDelete()
        {
            ConfirmConfig config = new ConfirmConfig();
            config.Message = Strings.DeleteMessageDialog;
            config.CancelText = Strings.NoButtonText;
            config.OkText = Strings.OkButtonText;
            return await UserDialogs.Instance.ConfirmAsync(config);
        }

        public void AlertMessage(string message)
        {
            AlertConfig alert = new AlertConfig();
            alert.Message = message;
            UserDialogs.Instance.Alert(alert);
        }

        // TODO: Write logic after creating viewmodel
        public ConfirmConfig ConfirmCancel()
        {
            ConfirmConfig config = new ConfirmConfig();
            config.Message = Strings.SaveChangesDialog;
            //ConfirmConfig.DefaultNo = Strings.NoButtonText;
            //ConfirmConfig.DefaultYes = Strings.OkButtonText;
            //ConfirmConfig.DefaultCancelText = Strings.CancelButtonText;
            //config.UseYesNo();
            return config;
        }
    }
}
