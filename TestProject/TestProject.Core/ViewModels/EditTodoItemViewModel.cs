using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.Enums;
using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : TodoItemViewModel, IMvxViewModel<TodoItem, DeletionResult<TodoItem>>
    {
        private TodoItem _unmodifiedTodoItem;

        public EditTodoItemViewModel(IMvxNavigationService navigationService,  IDialogsHelper dialogsHelper,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository)
            : base(navigationService, validationHelper, todoItemRepository, dialogsHelper)
        {
            UpdateTodoItemCommand = new MvxAsyncCommand(UpdateTodoItem);
            DeleteTodoItemCommand = new MvxAsyncCommand(DeleteTodoItem);
        }        
        
        public IMvxAsyncCommand UpdateTodoItemCommand { get; private set; }

        public IMvxAsyncCommand DeleteTodoItemCommand { get; private set; }

        protected override bool IsStateChanged
        {
            get
            {
                return Description != TodoItem.Description 
                    || IsDone != TodoItem.IsDone;
            }
        }

        public void Prepare(TodoItem parameter)
        {
            TodoItem = parameter;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            _name = TodoItem.Name;
            _description = TodoItem.Description;
            _isDone = TodoItem.IsDone;

            _unmodifiedTodoItem = new TodoItem
            {
                Description = Description,
                IsDone = IsDone
            };
        }

        protected override async Task GoBack()
        {
            if (!IsStateChanged)
            {
                await _navigationService.Close<DeletionResult<TodoItem>>(this, result: null);
                return;
            }

            YesNoCancelDialogResult result = await _navigationService
                .Navigate<CancelDialogViewModel, YesNoCancelDialogResult>();

            await Task.Delay(600);

            if (result == YesNoCancelDialogResult.Yes)
            {
                await UpdateTodoItem();
                return;
            }

            await HandleDialogResult(result);
        }

        private async Task UpdateTodoItem()
        {
            ChangeTodoItem();
            bool isTodoItemValid = await IsDataValid();
            if (!isTodoItemValid)
            {
                TodoItem.Description = _unmodifiedTodoItem.Description;
                TodoItem.IsDone = _unmodifiedTodoItem.IsDone;
                return;
            }

            await _todoItemRepository.Update(TodoItem);
            await _navigationService.Close<DeletionResult<TodoItem>>(this, result:null);
        }

        private void ChangeTodoItem()
        {
            TodoItem.Description = Description;
            TodoItem.IsDone = IsDone;
        }

        private async Task DeleteTodoItem()
        {
            bool isToDelete = await _dialogsHelper.IsConfirmed(Strings.DeleteMessageDialog);

            if (!isToDelete)
            {
                return;
            }

            await _todoItemRepository.Delete(TodoItem);

            DeletionResult<TodoItem> deletionResult = GetDeletionResult(TodoItem);
            await _navigationService.Close(this, deletionResult);
        }
    }
}
