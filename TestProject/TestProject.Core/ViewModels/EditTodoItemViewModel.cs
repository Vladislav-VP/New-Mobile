using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Entities;
using TestProject.Services;
using TestProject.Core.ViewModelResults;
using System.Threading.Tasks;
using MvvmCross.Commands;
using TestProject.Services.Helpers;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Resources;
using Acr.UserDialogs;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : TodoItemViewModel, IMvxViewModel<TodoItem>
    {
        private TodoItem _todoItem;

        public EditTodoItemViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(navigationService, userDialogs)
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
            ChangeAllProperties();
            DataValidationHelper validationHelper = new DataValidationHelper();
            if (!validationHelper.TodoItemIsValid(TodoItem))
            {
                _dialogsHelper.ToastMessage(Strings.EmptyTodoItemNameMessage);
                return false;
            }

            await _todoItemRepository.Update(TodoItem);
            return true;
        }

        private void ChangeAllProperties()
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
