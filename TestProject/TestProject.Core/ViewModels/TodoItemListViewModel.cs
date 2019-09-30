using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class TodoItemListViewModel : BaseViewModel
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemListViewModel(IMvxNavigationService navigationService,
            IUserStorageHelper storage, ITodoItemRepository todoItemRepository)
            : base(navigationService, storage)
        {
            _todoItemRepository = todoItemRepository;

            ShowMenuCommand = new MvxAsyncCommand(async ()
                  => await _navigationService.Navigate<MenuViewModel>());
            AddTodoItemCommand = new MvxAsyncCommand<TodoItem>(AddTodoItem);
            SelectTodoItemCommand = new MvxAsyncCommand<TodoItem>(SelectTodoItem);
            RefreshTodoItemsCommand = new MvxCommand(RefreshTodoItems);
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
            User currentUser = await _storage.Get();

            TodoItems = new MvxObservableCollection<TodoItem>(await _todoItemRepository.GetTodoItems(currentUser.Id));
            LoadTodoItemsTask = MvxNotifyTask.Create(LoadTodoItems);
        }

        public IMvxAsyncCommand<TodoItem> AddTodoItemCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        public IMvxCommand RefreshTodoItemsCommand { get; private set; }

        public IMvxCommand<TodoItem> SelectTodoItemCommand { get; private set; }

        public MvxNotifyTask LoadTodoItemsTask { get; private set; }

        private async Task SelectTodoItem(TodoItem selectedTodoItem)
        {
            Func<TodoItem, Task<ViewModelResult<TodoItem>>> todoItemHandleResult;
            todoItemHandleResult = async (todoItem) => await _navigationService
                 .Navigate<EditTodoItemViewModel, TodoItem, ViewModelResult<TodoItem>>(todoItem);

            ViewModelResult<TodoItem> result = await todoItemHandleResult(selectedTodoItem);
            
            if (result is DeletionResult<TodoItem> && result.IsSucceded)
            {
                TodoItems.Remove(selectedTodoItem);
                return;
            }

            if(result is UpdateResult<TodoItem> && result.IsSucceded)
            {
                int index = TodoItems.IndexOf(selectedTodoItem);
                TodoItems.Insert(index, result.Entity);
                TodoItems.Remove(selectedTodoItem);
            }
        }

        private async Task AddTodoItem(TodoItem todoItem)
        {
            ViewModelResult<TodoItem> creationResult = await _navigationService
                .Navigate<CreateTodoItemViewModel, CreationResult<TodoItem>>();
            
            if (creationResult != null && creationResult.IsSucceded)
            {
                TodoItems.Add(creationResult.Entity);
            }
        }

        private async Task LoadTodoItems()
        {
            var mockedTodoItems = new List<TodoItem>();
            mockedTodoItems.AddRange(TodoItems);
            TodoItems.Clear();
            
            int delayTime = 500;
            foreach (TodoItem todoItem in mockedTodoItems)
            {
                await Task.Delay(delayTime);
                TodoItems.Add(todoItem);
            }
        }

        private void RefreshTodoItems()
        {
            LoadTodoItemsTask = MvxNotifyTask.Create(LoadTodoItems);
            RaisePropertyChanged(() => LoadTodoItemsTask);
        }
    }
}
