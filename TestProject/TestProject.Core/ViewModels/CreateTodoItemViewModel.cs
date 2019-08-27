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

        public CreateTodoItemViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, IValidationHelper validationHelper,
            IValidationResultHelper validationResultHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, validationHelper, validationResultHelper, todoItemRepository, dialogsHelper)
        {
            CreateTodoItemCommand = new MvxAsyncCommand(CreateTodoItem);
        }
        
        public IMvxAsyncCommand CreateTodoItemCommand { get; private set; }
        
        protected async override Task GoBack()
        {
            DialogResult result = await _navigationService.Navigate<CancelDialogViewModel, DialogResult>();

            if (result == DialogResult.Yes)
            {
                await CreateTodoItem();
                return;
            }

            await HandleDialogResult(result);
        }


        private async Task CreateTodoItem()
        {
            bool isTodoItemValid = await TryValidateData();
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
