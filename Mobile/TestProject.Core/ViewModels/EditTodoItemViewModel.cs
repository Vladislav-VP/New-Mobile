using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : TodoItemViewModel, IMvxViewModel<TEntity, ViewModelResult<TEntity>>,
        IMvxViewModel<TEntity, DeletionResult<TEntity>>, IMvxViewModel<TEntity, UpdateResult<TEntity>>
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

        public void Prepare(TEntity parameter)
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

            var deletionResult = new DeletionResult<TEntity>
            {
                IsSucceded = true
            };

            await _navigationService.Close(this, deletionResult);
        }

        protected override async Task HandleEntity()
        {
            //var todoItem = new TodoItem
            //{
            //    Id = _todoItemId,
            //    Name = Name,
            //    Description = Description,
            //    IsDone = IsDone,
            //    UserId = _userId
            //};

            TEntity todoItem = await _todoItemService.Get(_todoItemId);
            todoItem.Description = Description;
            todoItem.IsDone = IsDone;

            bool isTodoItemValid = _validationHelper.IsObjectValid(todoItem);
            if (!isTodoItemValid)
            {
                return;
            }

            await _todoItemService.Update(todoItem);
            //await _todoItemRepository.Update(todoItem);
            var updateResult = new UpdateResult<TEntity>
            {
                Entity = todoItem,
                IsSucceded = true
            };
            await _navigationService.Close(this, updateResult);
        }
    }
}
