using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : TodoItemViewModel, IMvxViewModel<TodoItem, CreationResult<TodoItem>>
    {
        public CreateTodoItemViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, 
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, validationHelper, todoItemRepository, dialogsHelper)
        {
            CreateTodoItemCommand = new MvxAsyncCommand(HandleEntity);
        }
        
        public IMvxAsyncCommand CreateTodoItemCommand { get; private set; }

        protected override bool IsStateChanged
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Name) 
                    || !string.IsNullOrWhiteSpace(Description)
                    || IsDone;
            }
        }

        public void Prepare(TodoItem parameter)
        {
            TodoItem = parameter;
        }

        private async Task AddTodoItem()
        {
            User currentUser = await _storage.Get();
            TodoItem.Name = Name;
            TodoItem.Description = Description;
            TodoItem.IsDone = IsDone;
            TodoItem.UserId = currentUser.Id;

            await _todoItemRepository.Insert(TodoItem);
        }

        protected override async Task HandleEntity()
        {
            bool isTodoItemValid = await IsDataValid();
            if (!isTodoItemValid)
            {
                return;
            }

            await AddTodoItem();

            CreationResult<TodoItem> creationResult = GetCreationResult(TodoItem);
            await _navigationService.Close(this, creationResult);
        }
    }
}
