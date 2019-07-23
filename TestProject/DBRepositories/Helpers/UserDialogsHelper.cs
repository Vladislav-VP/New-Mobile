using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Configurations;
using TestProject.Services.Resources;

namespace TestProject.Services.Helpers
{
    public static class UserDialogsHelper
    {


        public static void ToastErrorMessage(string message)
        {
            ToastConfig toast = new ToastConfig(message);
            toast.SetDuration(Constants.ToastDuration);
            toast.SetPosition(ToastPosition.Top);
            UserDialogs.Instance.Toast(toast);
        }

        public static ConfirmConfig DeleteDialogConfig()
        {
            ConfirmConfig config = new ConfirmConfig();
            config.CancelText = Strings.NoButtonText;
            config.Message = Strings.DeleteMessageDialog;
            config.OkText = Strings.OkButtonText;
            return config;
        }
    }
}
