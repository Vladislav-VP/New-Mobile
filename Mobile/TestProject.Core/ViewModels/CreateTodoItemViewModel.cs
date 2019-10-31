using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : TodoItemViewModel, IMvxViewModelResult<CreationResult<TodoItem>>
    {
        public CreateTodoItemViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, ICancelDialogService cancelDialogService, IUserService userService,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper, ITodoItemService todoItemService)
            : base(navigationService, storage, cancelDialogService, validationHelper, todoItemRepository, dialogsHelper, todoItemService)
        {
            CreateTodoItemCommand = new MvxAsyncCommand(HandleEntity);
        }
        
        public IMvxAsyncCommand CreateTodoItemCommand { get; private set; }

        protected override async Task HandleEntity()
        {
            int userId = await _storage.Get();
            var todoItem = new TodoItem
            {
                Name = Name,
                Description = Description,
                IsDone = IsDone,
                UserId = userId
            };
            DataHandleResult<TodoItem> result = await _todoItemService.CreateTodoItem(todoItem);

            // TODO : Refactor result.
            var creationResult = new CreationResult<TodoItem>
            {
                Entity = todoItem,
                IsSucceded = true
            };

            await _navigationService.Close(this, result: creationResult);
        }
    }
}
