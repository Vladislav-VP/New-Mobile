using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Entity;

namespace TestProject.Core.ViewModels
{

    public class TodoListItemViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService _navigationService;

        private MvxObservableCollection<TodoItem> _items;

        public MvxObservableCollection<TodoItem> Items
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
