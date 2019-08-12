using MvvmCross.Navigation;
using System.Threading.Tasks;
using MvvmCross.Commands;
using TestProject.Entities;
using TestProject.Services.Helpers;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Resources;
using TestProject.Core.ViewModelResults;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : TodoItemViewModel
    {

        public CreateTodoItemViewModel(IMvxNavigationService navigationService, IStorageHelper<User> storage,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, validationHelper, todoItemRepository, dialogsHelper)
        {
            BackToListCommand = new MvxAsyncCommand(async ()
               => await _navigationService.Navigate<TodoListItemViewModel>());
            TodoItemCreatedCommand = new MvxAsyncCommand(TodoItemCreated);
        }
        
        public IMvxAsyncCommand TodoItemCreatedCommand { get; private set; }

        public IMvxAsyncCommand BackToListCommand { get; private set; }

        protected async override Task GoBack()
        {
            var result = await _navigationService.Navigate<CancelDialogViewModel, DialogResult>();

            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (result == DialogResult.No)
            {
                await _navigationService.Close(this);
                return;
            }
            if (result == DialogResult.Yes)
            {
                await TodoItemCreated();
                return;
            }
        }

        private async Task TodoItemCreated()
        {
            TodoItem todoItem = new TodoItem { Name = Name };

            bool todoItemIsValid = _validationHelper.ObjectIsValid<TodoItem>(todoItem);
            bool validationErrorsEmpty = _validationHelper.ValidationErrors.Count == 0;
            if (!todoItemIsValid && !validationErrorsEmpty)
            {
                _dialogsHelper.ToastMessage(_validationHelper.ValidationErrors[0].ErrorMessage);
                return;
            }

            await AddTodoItem();
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }    
        
        private async Task AddTodoItem()
        {
            User currentUser = await _storage.Retrieve();
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
