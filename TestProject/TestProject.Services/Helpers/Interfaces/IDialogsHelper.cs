using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IDialogsHelper
    {
        void ToastMessage(string message, int duration = 3000);

        ConfirmConfig ConfirmDelete();
    }
}
