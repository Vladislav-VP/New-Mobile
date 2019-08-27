using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.Enums;

namespace TestProject.Core.ViewModels
{
    public class CancelDialogViewModel : BaseViewModel, IMvxViewModelResult<DialogResult>
    {
        private DialogResult _dialogResult = DialogResult.Cancel;

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
            _dialogResult = DialogResult.Yes;
            await _navigationService.Close(this, _dialogResult);
        }

        private async Task DoNotSave()
        {
            _dialogResult = DialogResult.No;
            await _navigationService.Close(this, _dialogResult);
        }

        private async Task Cancel()
        {
            _dialogResult = DialogResult.Cancel;
            await _navigationService.Close(this, _dialogResult);
        }
    }
}
