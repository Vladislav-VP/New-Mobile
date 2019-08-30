using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Core.Enums;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : TodoItemViewModel
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

        protected async override Task GoBack()
        {
            if (!IsStateChanged)
            {
                await _navigationService.Navigate<TodoListItemViewModel>();
                return;
            }

            YesNoCancelDialogResult result = await _navigationService.Navigate<CancelDialogViewModel, YesNoCancelDialogResult>();

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
            await _navigationService.Navigate<TodoListItemViewModel>();
        }    
        
        private async Task AddTodoItem()
        {
            User currentUser = await _storage.Get();
            TodoItem todoItem = new TodoItem
            {
                Name = Name,
                Description = Description,
                IsDone = IsDone,
                UserId = currentUser.Id
            };
            await _todoItemRepository.Insert(todoItem);
        }


    }
}
