using MvvmCross.Navigation;
using System.Threading.Tasks;
using MvvmCross.Commands;
using TestProject.Entities;
using TestProject.Services.Helpers;
using Acr.UserDialogs;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : TodoItemViewModel
    {
        public CreateTodoItemViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
        {
            BackToListCommand = new MvxAsyncCommand(async ()
               => await _navigationService.Navigate<TodoListItemViewModel>());
            TodoItemCreatedCommand = new MvxAsyncCommand(ItemCreated);
        }
        
        public IMvxAsyncCommand TodoItemCreatedCommand { get; private set; }

        public IMvxAsyncCommand BackToListCommand { get; private set; }

        private async Task ItemCreated()
        {
            TodoItem todoItem = new TodoItem { Name = Name };

            DataValidationHelper validationHelper = new DataValidationHelper();
            if (!validationHelper.TodoItemIsValid(todoItem))
            {
                _dialogsHelper.ToastMessage(validationHelper.ValidationErrors[0].ErrorMessage);
                return;
            }

            await AddTodoItem();
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }    
        
        private async Task AddTodoItem()
        {
            User currentUser = await _storage.Load();
            TodoItem todoItem = new TodoItem { Name = Name, Description = Description,
                IsDone = IsDone, UserId = currentUser.Id };
            await _todoItemRepository.Insert(todoItem);
        }

        protected async override Task GoBack()
        {
            await _userDialogs.ConfirmAsync(_dialogsHelper.ConfirmCancel());
            await base.GoBack();
        }
    }
}
