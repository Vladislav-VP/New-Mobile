using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Entities;
using System.Threading.Tasks;
using MvvmCross.Commands;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Resources;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : TodoItemViewModel, IMvxViewModel<TodoItem>
    {
        private TodoItem _todoItem;

        public EditTodoItemViewModel(IMvxNavigationService navigationService, IValidationHelper validationHelper,
            ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, validationHelper, todoItemRepository, dialogsHelper)
        {
            TodoItemUpdatedCommand = new MvxAsyncCommand(TodoItemUpdated);
            TodoItemDeletedCommand = new MvxAsyncCommand(TodoItemDeleted);
        }

        public TodoItem TodoItem
        {
            get => _todoItem;
            set
            {
                _todoItem = value;
                RaisePropertyChanged(() => TodoItem);
            }
        }

        public IMvxAsyncCommand TodoItemUpdatedCommand { get; private set; }

        public IMvxAsyncCommand TodoItemDeletedCommand { get; private set; }

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
        }

        private async Task TodoItemUpdated()
        {
            if(!await TryUpdateTodoItem())
            {
                return;
            }

            var result = await _navigationService.Close(this);
        }

        private async Task<bool> TryUpdateTodoItem()
        {
            ChangeTodoItem();
            
            bool todoItemIsValid = _validationHelper.ObjectIsValid<TodoItem>(TodoItem);
            bool validationErrorsEmpty = _validationHelper.ValidationErrors.Count == 0;
            if (!todoItemIsValid && !validationErrorsEmpty)
            {
                _dialogsHelper.ToastMessage(_validationHelper.ValidationErrors[0].ErrorMessage);
                return false;
            }

            await _todoItemRepository.Update(TodoItem);
            return true;
        }

        private void ChangeTodoItem()
        {
            TodoItem.Name = Name;
            TodoItem.Description = Description;
            TodoItem.IsDone = IsDone;
        }

        private async Task TodoItemDeleted()
        {
            var delete = await _dialogsHelper.ConfirmDelete();

            if (!delete)
            {
                return;
            }

            await _todoItemRepository.Delete(TodoItem);

            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
