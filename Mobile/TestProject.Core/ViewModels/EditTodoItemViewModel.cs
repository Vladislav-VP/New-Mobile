using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : TodoItemViewModel, IMvxViewModel<TodoItem, ViewModelResult<TodoItem>>,
        IMvxViewModel<TodoItem, DeletionResult<TodoItem>>, IMvxViewModel<TodoItem, UpdateResult<TodoItem>>
    {
        private int _todoItemId;
        private int _userId;

        public EditTodoItemViewModel(IMvxNavigationService navigationService,  IDialogsHelper dialogsHelper,
            ICancelDialogService cancelDialogService, IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, ITodoItemService webService)
            : base(navigationService, validationHelper, cancelDialogService, todoItemRepository, dialogsHelper, webService)
        {
            UpdateTodoItemCommand = new MvxAsyncCommand(HandleEntity);
            DeleteTodoItemCommand = new MvxAsyncCommand(DeleteTodoItem);
        }        
        
        public IMvxAsyncCommand UpdateTodoItemCommand { get; private set; }

        public IMvxAsyncCommand DeleteTodoItemCommand { get; private set; }

        public void Prepare(TodoItem parameter)
        {
            _todoItemId = parameter.Id;
            Name = parameter.Name;
            Description = parameter.Description;
            IsDone = parameter.IsDone;
            _userId = parameter.UserId;
        }
        
        private async Task DeleteTodoItem()
        {
            bool isConfirmedToDelete = await _dialogsHelper.IsConfirmed(Strings.DeleteMessageDialog);

            if (!isConfirmedToDelete)
            {
                return;
            }

            //await _todoItemRepository.Delete<TodoItem>(_todoItemId);
            await _todoItemService.Delete(_todoItemId);

            // TODO : Refactor result.
            var deletionResult = new DeletionResult<TodoItem>
            {
                IsSucceded = true
            };

            await _navigationService.Close(this, deletionResult);
        }

        protected override async Task HandleEntity()
        {

            TodoItem todoItem = await _todoItemService.Get(_todoItemId);

            DataHandleResult<TodoItem> result = await _todoItemService.EditTodoItem(todoItem, Description, IsDone);
            if (!result.IsSucceded)
            {
                return;
            }
            //await _todoItemRepository.Update(todoItem);
            var updateResult = new UpdateResult<TodoItem>
            {
                Entity = todoItem,
                IsSucceded = true
            };
            await _navigationService.Close(this, updateResult);
        }
    }
}
