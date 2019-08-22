using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Core.ViewModelResults;
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
        
        protected async override Task GoBack()
        {
            DialogResult result = await _navigationService.Navigate<CancelDialogViewModel, DialogResult>();

            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (result == DialogResult.No)
            {
                await _navigationService.Navigate<TodoListItemViewModel>();
                return;
            }
            if (result == DialogResult.Yes)
            {
                await CreateTodoItem();
                return;
            }
        }

        private async Task CreateTodoItem()
        {
            TodoItem todoItem = new TodoItem { Name = Name };

            bool todoItemIsValid = _validationHelper.ObjectIsValid<TodoItem>(todoItem);
            bool validationErrorsEmpty = _validationHelper.ValidationErrors.Count == 0;
            if (!todoItemIsValid && !validationErrorsEmpty)
            {
                _dialogsHelper.DisplayToastMessage(_validationHelper.ValidationErrors[0].ErrorMessage);
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
