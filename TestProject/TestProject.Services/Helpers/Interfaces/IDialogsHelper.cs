using System.Threading.Tasks;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IDialogsHelper
    {
        void DisplayToastMessage(string message, int duration = 3000);

        void DisplayAlertMessage(string message);

        Task<bool> TryGetConfirmation(string message);
    }
}
