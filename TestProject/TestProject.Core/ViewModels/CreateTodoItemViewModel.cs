using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using MvvmCross.Commands;
using TestProject.Entities;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services;
using TestProject.Configurations;
using Acr.UserDialogs;
using TestProject.Core.Resources;
using TestProject.Services.Helpers;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : TodoItemViewModel
    {
        private readonly ITodoItemRepository _todoItemRepository;       

        public CreateTodoItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _todoItemRepository = new TodoItemRepository();

            BackToListCommand = new MvxAsyncCommand(async ()
               => await _navigationService.Navigate<TodoListItemViewModel>());
            TodoItemCreatedCommand = new MvxAsyncCommand(ItemCreated);
        }
        
        public IMvxAsyncCommand TodoItemCreatedCommand { get; private set; }

        public IMvxAsyncCommand BackToListCommand { get; private set; }

        private async Task ItemCreated()
        {
            TodoItem todoItem = new TodoItem { Name = Name };

            List<ValidationResult> validationResults;
            if (!new DataValidationHelper().TodoItemIsValid(todoItem, out validationResults))
            {
                new UserDialogsHelper().ToastErrorMessage(validationResults[0].ErrorMessage);
                return;
            }

            await AddTodoItem();
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }

        private bool TodoItemIsValid()
        {
            if(string.IsNullOrWhiteSpace(Name))
            {
                
                return false;
            }

            return true;
        }
        
        private async Task AddTodoItem()
        {
            User currentUser = await new CredentialsStorageHelper().Load();
            TodoItem todoItem = new TodoItem { Name = Name, Description = Description,
                IsDone = IsDone, UserId = currentUser.Id };
            await _todoItemRepository.Insert(todoItem);
        }
    }
}
