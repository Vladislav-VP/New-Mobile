using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.Enums;
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
            CreateTodoItemCommand = new MvxAsyncCommand(CreateTodoItem);
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

        protected async override Task GoBack()
        {
            if (!IsStateChanged)
            {
                await _navigationService.Close<CreationResult<TodoItem>>(this, result: null);
                return;
            }

            YesNoCancelDialogResult result = 
                await _navigationService.Navigate<CancelDialogViewModel, YesNoCancelDialogResult>();

            if (result == YesNoCancelDialogResult.Yes)
            {
                await CreateTodoItem();
                return;
            }

            await HandleDialogResult(result);
        }

        private async Task CreateTodoItem()
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

        private async Task AddTodoItem()
        {
            User currentUser = await _storage.Get();
            TodoItem.Name = Name;
            TodoItem.Description = Description;
            TodoItem.IsDone = IsDone;
            TodoItem.UserId = currentUser.Id;

            await _todoItemRepository.Insert(TodoItem);
        }
    }
}
