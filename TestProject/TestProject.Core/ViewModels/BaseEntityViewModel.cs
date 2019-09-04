using System.Threading.Tasks;

using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.Enums;
using TestProject.Core.MvxInteraction;
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

        public MvxInteraction<CreationAction> CreationInteraction { get; set; } = new MvxInteraction<CreationAction>();

        public MvxInteraction<UpdateAction> UpdateInteraction { get; set; } = new MvxInteraction<UpdateAction>();

        public MvxInteraction<DeletionAction> DeletionInteraction { get; set; } = new MvxInteraction<DeletionAction>();

        protected virtual bool IsStateChanged
        {
            get => false;
        }

        protected abstract Task<bool> IsDataValid();

        protected virtual CreationAction GetCreationRequest(BaseEntity entity)
        {
            var creationRequest = new CreationAction();
            var creationResult = new CreationResult<BaseEntity>
            {
                Entity = entity,
                IsCreated = true
            };

            creationRequest.OnCreated = () => _navigationService
                .Close((IMvxViewModelResult<CreationResult<BaseEntity>>)this, creationResult);

            return creationRequest;
        }

        protected virtual UpdateAction GetUpdateRequest(BaseEntity entity)
        {
            var updateRequest = new UpdateAction();
            var updateResult = new UpdateResult<BaseEntity>
            {
                Entity = entity,
                IsUpdated = true
            };

            updateRequest.OnUpdated = async () => await _navigationService
                .Close((IMvxViewModelResult<UpdateResult<BaseEntity>>)this, updateResult);

            return updateRequest;
        }

        protected virtual DeletionAction GetDeleteRequest(BaseEntity entity)
        {
            var deletionRequest = new DeletionAction();
            var deletionResult = new DeletionResult<BaseEntity>
            {
                Entity = entity,
                IsDeleted = true
            };

            deletionRequest.OnDeleted = () => _navigationService
              .Close((IMvxViewModelResult<DeletionResult<BaseEntity>>)this, deletionResult);

            return deletionRequest;
        }

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
