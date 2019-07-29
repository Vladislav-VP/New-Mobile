using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Configurations;
using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services;
using TestProject.Services.Repositories;
using TestProject.Services.Repositories.Interfaces;
using System.IO;
using TestProject.Services.Helpers;

namespace TestProject.Core.ViewModels
{

    public class TodoListItemViewModel : BaseViewModel
    {
        private readonly ITodoItemRepository _todoItemRepository;

        private MvxObservableCollection<TodoItem> _todoItems;

        public TodoListItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService)
        {
            _todoItemRepository = new TodoItemRepository();

            ShowCreateTodoItemViewModelCommand = new MvxAsyncCommand(async ()
                => await _navigationService.Navigate<CreateTodoItemViewModel>());
            TodoItemSelectedCommand = new MvxAsyncCommand<TodoItem>(TodoItemSelected);
        }
        
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
            
            User currentUser = await _storage.Load();
            TodoItems = new MvxObservableCollection<TodoItem>(await _todoItemRepository.GetTodoItems(currentUser.Id));
        }

        public IMvxAsyncCommand ShowCreateTodoItemViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

        public IMvxCommand<TodoItem> TodoItemSelectedCommand { get; private set; }

        private async Task TodoItemSelected(TodoItem selectedTodoItem)
        {
            var result = await _navigationService
                .Navigate<TodoItem>(typeof(EditTodoItemViewModel), selectedTodoItem);
        }
    }
}
