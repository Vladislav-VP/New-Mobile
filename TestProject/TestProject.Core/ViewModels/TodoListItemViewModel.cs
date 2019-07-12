using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Entities;

namespace TestProject.Core.ViewModels
{

    public class TodoListItemViewModel : BaseViewModel
    {
        public TodoListItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            AddTodoItemCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<CreateTodoItemViewModel>());
            ShowMenuCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }

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

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }
    }
}
