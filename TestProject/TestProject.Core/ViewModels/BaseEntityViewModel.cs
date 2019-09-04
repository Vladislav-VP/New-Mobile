using System.Threading.Tasks;

using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.Enums;
using TestProject.Core.ViewModelResults;
using TestProject.Core.ViewModelResults.Interfaces;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class BaseEntityViewModel : BaseViewModel, IMvxViewModelResult<IViewModelResult<BaseEntity>>
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
        
        public TaskCompletionSource<object> CloseCompletionSource { get; set; }

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
                await _navigationService.Close(this, result: null);
                return;
            }
        }

        protected virtual CreationResult<TEntity> GetCreationResult<TEntity>(TEntity entity)
        {
            var creationResult = new CreationResult<TEntity>
            {
                Entity = entity,
                IsCreated = true
            };

            return creationResult;
        }

        protected virtual UpdateResult<TEntity> GetUpdateResult<TEntity>(TEntity entity)
        {
            var updateResult = new UpdateResult<TEntity>
            {
                Entity = entity,
                IsUpdated = true
            };

            return updateResult;
        }

        protected virtual DeletionResult<TEntity> GetDeletionResult<TEntity>(TEntity entity)
        {
            var deletionResult = new DeletionResult<TEntity>
            {
                Entity = entity,
                IsDeleted = true
            };

            return deletionResult;
        }
    }
}
