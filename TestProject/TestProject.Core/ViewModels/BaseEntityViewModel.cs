using System.Threading.Tasks;

using MvvmCross.Navigation;

using TestProject.Core.Enums;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseEntityViewModel : BaseViewModel
    {
        protected readonly IValidationHelper _validationHelper;
        
        protected readonly IDialogsHelper _dialogsHelper;

        public BaseEntityViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage,
            IDialogsHelper dialogsHelper, IValidationHelper validationHelper)
            : base(navigationService, storage)
        {
            _dialogsHelper=dialogsHelper;
            _validationHelper = validationHelper;
        }

        protected virtual bool IsStateChanged
        {
            get => false;
        }

        protected abstract Task<bool> IsDataValid();

        protected virtual async Task HandleDialogResult(YesNoCancelDialogResult result)
        {
            if (result == YesNoCancelDialogResult.Cancel)
            {
                return;
            }
            if (result == YesNoCancelDialogResult.No)
            {
                await _navigationService.Navigate<TodoListItemViewModel>();
                return;
            }
        }
    }
}
