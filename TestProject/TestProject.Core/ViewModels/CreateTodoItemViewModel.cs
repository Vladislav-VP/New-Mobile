using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using System.Threading.Tasks;
using MvvmCross.Commands;
using TestProject.Repositories;
using TestProject.Repositories.Interfaces;
using TestProject.Entity;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : BaseTodoItemViewModel
    {
        private readonly IGenericRepository<TodoItem> _genericTodoItemRepository;

        public CreateTodoItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _genericTodoItemRepository = new GenericRepository<TodoItem>();

            BackToListCommand = new MvxAsyncCommand(GoBack);
            CreateTodoItemCommand = new MvxAsyncCommand(CreateItem);
        }

        public IMvxAsyncCommand CreateTodoItemCommand { get; private set; }

        public IMvxAsyncCommand BackToListCommand { get; private set; }


        private async Task GoBack()
        {
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }

        private async Task CreateItem()
        {
            await _genericTodoItemRepository.Insert(Item);
            var result = await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
