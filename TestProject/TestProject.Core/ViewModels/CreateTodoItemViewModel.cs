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

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : TodoItemViewModel
    {
        private readonly ITodoItemRepository _todoItemRepository;       

        public CreateTodoItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _todoItemRepository = new TodoItemRepository();

            BackToListCommand = new MvxAsyncCommand(GoBack);
            TodoItemCreatedCommand = new MvxAsyncCommand(CreateItem);
        }
        
        public IMvxAsyncCommand TodoItemCreatedCommand { get; private set; }

        public IMvxAsyncCommand BackToListCommand { get; private set; }

        private async Task GoBack()
        {
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }

        private async Task CreateItem()
        {
            TodoItem item = new TodoItem { Name = this.Name, Description = this.Description,
                IsDone = this.IsDone, UserId = StaticObjects.CurrentUser.Id };
            await _todoItemRepository.Insert(item);
            StaticObjects.CurrentTodoItems = await _todoItemRepository.GetTodoItems(StaticObjects.CurrentUser.Id);
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
