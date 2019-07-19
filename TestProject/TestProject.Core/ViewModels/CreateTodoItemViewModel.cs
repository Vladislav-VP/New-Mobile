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
using TestProject.Configurations.Interfaces;
using TestProject.Configurations;

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
            TodoItemCreatedCommand = new MvxAsyncCommand(CreateItem);
        }
        
        public IMvxAsyncCommand TodoItemCreatedCommand { get; private set; }

        public IMvxAsyncCommand BackToListCommand { get; private set; }

        private async Task CreateItem()
        {
            ILocalStorage<User> storage = new LocalStorage<User>();
            User currentUser = storage.Get();
            TodoItem item = new TodoItem { Name = Name, Description = Description,
                IsDone = this.IsDone, UserId = currentUser.Id };
            await _todoItemRepository.Insert(item);
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
