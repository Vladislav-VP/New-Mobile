using System.Threading.Tasks;

using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.Enums;
using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseEntityViewModel : BaseViewModel, IMvxViewModelResult<ViewModelResult<BaseEntity>>
    {
        protected readonly IValidationHelper _validationHelper;
        
        protected readonly IDialogsHelper _dialogsHelper;

        public BaseEntityViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage,
            IDialogsHelper dialogsHelper, IValidationHelper validationHelper)
            : base(navigationService, storage)
        {
            _dialogsHelper = dialogsHelper;
            _validationHelper = validationHelper;
        }
        
        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

        protected abstract bool IsStateChanged { get; }

        protected abstract Task<bool> IsDataValid();

        protected async Task HandleDialogResult(DialogResult result)
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

        // TODO: Think about better name.
        protected abstract Task HandleEntity();

        protected virtual CreationResult<TEntity> GetCreationResult<TEntity>(TEntity entity)
        {
            var creationResult = new CreationResult<TEntity>
            {
                Entity = entity,
                IsSucceded = true
            };

            return creationResult;
        }

        protected virtual UpdateResult<TEntity> GetUpdateResult<TEntity>(TEntity entity)
        {
            var updateResult = new UpdateResult<TEntity>
            {
                Entity = entity,
                IsSucceded = true
            };

            return updateResult;
        }

        protected virtual DeletionResult<TEntity> GetDeletionResult<TEntity>(TEntity entity)
        {
            var deletionResult = new DeletionResult<TEntity>
            {
                Entity = entity,
                IsSucceded = true
            };

            return deletionResult;
        }
    }
}
