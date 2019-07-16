using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Entities;
using TestProject.Services;

namespace TestProject.Core.ViewModels
{

    public class TodoListItemViewModel : BaseViewModel
    {
        public TodoListItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            AddTodoItemCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<CreateTodoItemViewModel>());
            ShowMenuViewModelCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MenuViewModel>());
        }

        private MvxObservableCollection<TodoItem> _todoItems;
        public MvxObservableCollection<TodoItem> TodoItems
        {
            get => _todoItems;
            set
            {
                _todoItems = value;
                RaisePropertyChanged(() => TodoItems);
            }
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            _todoItems = StaticObjects.TodoItems;
        }

        public IMvxAsyncCommand AddTodoItemCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }
    }
}
