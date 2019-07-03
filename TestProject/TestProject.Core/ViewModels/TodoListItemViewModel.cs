using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Core.Models;

namespace TestProject.Core.ViewModels
{

    public class TodoListItemViewModel : MvxViewModel
    {
        private IMvxNavigationService _navigationService;

        private MvxObservableCollection<TodoItemModel> _items;

        public MvxObservableCollection<TodoItemModel> Items
        {
            get => _items;
            set
            {
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        public IMvxAsyncCommand AddTodoItemCommand { get; private set; }

        public TodoListItemViewModel(IMvxNavigationService navigationService)
        {
            _navigationService = navigationService;

            AddTodoItemCommand = new MvxAsyncCommand(AddTodoItem);
        }

        private async Task AddTodoItem()
        {
            var result = await _navigationService.Navigate<CreateTodoItemViewModel>();
        }
    }
}
