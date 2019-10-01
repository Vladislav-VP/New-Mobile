using System.Threading.Tasks;

using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services.Enums;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseEntityViewModel : BaseViewModel, IMvxViewModelResult<ViewModelResult<BaseEntity>>
    {        
        protected readonly IDialogsHelper _dialogsHelper;

        protected readonly ICancelDialogService _cancelDialogService;

        public BaseEntityViewModel(IMvxNavigationService navigationService,
            IUserStorageHelper storage, IDialogsHelper dialogsHelper, ICancelDialogService cancelDialogService)
            : base(navigationService, storage)
        {
            _dialogsHelper = dialogsHelper;
            _cancelDialogService = cancelDialogService;
        }
        
        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        protected abstract bool IsStateChanged { get; }

        protected abstract Task HandleEntity();

        protected async override Task GoBack()
        {
            if (!IsStateChanged)
            {
                await _navigationService.Close(this, result: null);
                return;
            }

            DialogResult result = await _navigationService
                .Navigate<CancelDialogViewModel, DialogResult>();
            await _cancelDialogService.GoBack();

            if (result == DialogResult.Yes)
            {
                await HandleEntity();
                return;
            }

            await HandleDialogResult(result);
        }

        private async Task HandleDialogResult(DialogResult result)
        {
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (result == DialogResult.No)
            {
                await _navigationService.Close(this, result: null);
                return;
            }
        }
    }
}
