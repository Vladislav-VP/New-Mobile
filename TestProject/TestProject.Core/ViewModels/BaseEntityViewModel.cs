using System.Threading.Tasks;

using MvvmCross.Navigation;

using TestProject.Core.Enums;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseEntityViewModel : BaseViewModel
    {
        protected readonly IValidationHelper _validationHelper;

        protected readonly IValidationResultHelper _validationResultHelper;

        protected readonly IDialogsHelper _dialogsHelper;

        public BaseEntityViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage,
            IDialogsHelper dialogsHelper, IValidationHelper validationHelper, IValidationResultHelper validationResultHelper)
            : base(navigationService, storage)
        {
            _dialogsHelper=dialogsHelper;
            _validationHelper = validationHelper;
            _validationResultHelper = validationResultHelper;
        }

        protected abstract Task<bool> TryValidateData();

        protected virtual async Task HandleDialogResult(DialogResult result)
        {
            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (result == DialogResult.No)
            {
                await _navigationService.Navigate<TodoListItemViewModel>();
                return;
            }
        }
    }
}
