using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestProject.Configurations;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Resources;

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

        public ConfirmConfig ConfirmDelete()
        {
            ConfirmConfig config = new ConfirmConfig();
            config.CancelText = Strings.NoButtonText;
            config.Message = Strings.DeleteMessageDialog;
            config.OkText = Strings.OkButtonText;
            return config;
        }
    }
}
