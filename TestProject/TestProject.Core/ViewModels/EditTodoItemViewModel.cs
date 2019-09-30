using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : TodoItemViewModel, IMvxViewModel<TodoItem, ViewModelResult<TodoItem>>,
        IMvxViewModel<TodoItem, DeletionResult<TodoItem>>, IMvxViewModel<TodoItem, UpdateResult<TodoItem>>
    {
        private int _todoItemId;
        private int _userId;

        public EditTodoItemViewModel(IMvxNavigationService navigationService,  IDialogsHelper dialogsHelper,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository)
            : base(navigationService, validationHelper, todoItemRepository, dialogsHelper)
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
            bool isToDelete = await _dialogsHelper.IsConfirmed(Strings.DeleteMessageDialog);

            if (!isToDelete)
            {
                return;
            }

            TodoItem todoItem = await _todoItemRepository.Find(_todoItemId);

            await _todoItemRepository.Delete<TodoItem>(_todoItemId);

            DeletionResult<TodoItem> deletionResult = GetDeletionResult(todoItem);

            await _navigationService.Close(this, deletionResult);
        }

        protected override async Task HandleEntity()
        {
            var todoItem = new TodoItem
            {
                Id = _todoItemId,
                Name = Name,
                Description = Description,
                IsDone = IsDone,
                UserId = _userId
            };

            bool isTodoItemValid = _validationHelper.IsObjectValid(todoItem);
            if (!isTodoItemValid)
            {
                return;
            }

            await _todoItemRepository.Update(todoItem);
            UpdateResult<TodoItem> updateResult = GetUpdateResult(todoItem);
            await _navigationService.Close(this, updateResult);
        }
    }
}
