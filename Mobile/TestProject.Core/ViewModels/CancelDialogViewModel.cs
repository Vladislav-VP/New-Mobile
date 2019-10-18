using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Services.Enums;

namespace TestProject.Core.ViewModels
{
    public class CancelDialogViewModel : BaseViewModel, IMvxViewModelResult<DialogResult>
    {
        public CancelDialogViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            SaveCommand = new MvxAsyncCommand(Save);
            DoNotSaveCommand = new MvxAsyncCommand(DoNotSave);
            CancelCommand = new MvxAsyncCommand(Cancel);
        }

        public IMvxAsyncCommand SaveCommand { get; private set; }

        public IMvxAsyncCommand DoNotSaveCommand { get; private set; }

        public IMvxAsyncCommand CancelCommand { get; private set; }

        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        private async Task Save()
        {
            DialogResult result = DialogResult.Yes;
            await _navigationService.Close(this, result);
        }

        private async Task DoNotSave()
        {
            DialogResult result = DialogResult.No;
            await _navigationService.Close(this, result);
        }

        private async Task Cancel()
        {
            DialogResult result = DialogResult.Cancel;
            await _navigationService.Close(this, result);
        }
    }
}
