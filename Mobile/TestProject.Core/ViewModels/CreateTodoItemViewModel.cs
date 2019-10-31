using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : TodoItemViewModel, IMvxViewModelResult<CreationResult<TEntity>>
    {
        public CreateTodoItemViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, ICancelDialogService cancelDialogService, IUserService userService,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper, ITodoItemService webService)
            : base(navigationService, storage, cancelDialogService, userService, validationHelper, todoItemRepository, dialogsHelper, webService)
        {
            CreateTodoItemCommand = new MvxAsyncCommand(HandleEntity);
        }
        
        public IMvxAsyncCommand CreateTodoItemCommand { get; private set; }

        protected override async Task HandleEntity()
        {
            int userId = await _storage.Get();
            User currerntUser = await _userService.Get(userId);
            var todoItem = new TEntity
            {
                Name = Name,
                Description = Description,
                IsDone = IsDone,
                UserId = currerntUser.Id
            };

            bool isTodoItemValid = _validationHelper.IsObjectValid(todoItem);
            if (!isTodoItemValid)
            {
                return;
            }

            // TODO : Fix creating todoitems, move logic to service.
            //await _todoItemService.Post(todoItem, _url);
            await _todoItemRepository.Insert(todoItem);
            var creationResult = new CreationResult<TEntity>
            {
                Entity = todoItem,
                IsSucceded = true
            };

            await _navigationService.Close(this, result: creationResult);
        }
    }
}
