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
using TestProject.Configurations.Interfaces;

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

            User currentUser = GetUser();
            var currentTodoItems = await _todoItemRepository.GetTodoItems(currentUser.Id);
            _todoItems = new MvxObservableCollection<TodoItem>(currentTodoItems);
        }

        public IMvxAsyncCommand ShowCreateTodoItemViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

        public IMvxCommand<TodoItem> TodoItemSelectedCommand { get; private set; }

        private async Task TodoItemSelected(TodoItem selectedTodoItem)
        {
            // TODO : Write logic
            var result = await _navigationService
                .Navigate<EditTodoItemViewModel, TodoItem, DestructionResult<TodoItem>>(selectedTodoItem);
        }

        private User GetUser()
        {
            ILocalStorage<User> storage = new LocalStorage<User>();
            return storage.Get();
        }
    }
}
