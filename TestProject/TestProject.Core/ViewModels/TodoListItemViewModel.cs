using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services;
using TestProject.Services.Helpers;

namespace TestProject.Core.ViewModels
{

    public class TodoListItemViewModel : BaseViewModel
    {
        public TodoListItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            ShowCreateTodoItemViewModelCommand = new MvxAsyncCommand(async ()
                => await _navigationService.Navigate<CreateTodoItemViewModel>());
            TodoItemSelectedCommand = new MvxAsyncCommand<TodoItem>(TodoItemSelected);
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

            _todoItems = CastHelper<TodoItem>.ToMvxObservableCollection(StaticObjects.CurrentTodoItems);
        }

        public IMvxAsyncCommand ShowCreateTodoItemViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

        public IMvxCommand<TodoItem> TodoItemSelectedCommand { get; private set; }

        private async Task TodoItemSelected(TodoItem selectedTodoItem)
        {
            // Write logic
            var result = await _navigationService
                .Navigate<EditTodoItemViewModel, TodoItem, DestructionResult<TodoItem>>(selectedTodoItem);
        }

    }
}
