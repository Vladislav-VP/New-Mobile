using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.Enums;

namespace TestProject.Core.ViewModels
{
    public class CancelDialogViewModel : BaseViewModel, IMvxViewModelResult<YesNoCancelDialogResult>
    {
        private YesNoCancelDialogResult _dialogResult = YesNoCancelDialogResult.Cancel;

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
            _dialogResult = YesNoCancelDialogResult.Yes;
            await _navigationService.Close(this, _dialogResult);
        }

        private async Task DoNotSave()
        {
            _dialogResult = YesNoCancelDialogResult.No;
            await _navigationService.Close(this, _dialogResult);
        }

        private async Task Cancel()
        {
            _dialogResult = YesNoCancelDialogResult.Cancel;
            await _navigationService.Close(this, _dialogResult);
        }
    }
}
