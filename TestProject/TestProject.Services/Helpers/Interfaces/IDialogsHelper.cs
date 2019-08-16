using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IDialogsHelper
    {
        void ToastMessage(string message, int duration = 3000);

        void AlertMessage(string message);

        Task<bool> Confirm(string message);

        Task<EditPhotoDialogResult> ChooseOption();
    }
}
