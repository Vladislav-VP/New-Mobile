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
            DeletionResult<TodoItem> deletionResult = await _navigationService
                .Navigate<EditTodoItemViewModel, TodoItem, DeletionResult<TodoItem>>(selectedTodoItem);

            if (deletionResult != null && deletionResult.IsSucceded)
            {
                TodoItems.Remove(selectedTodoItem);
            }
        }

        private async Task AddTodoItem(TodoItem todoItem)
        {
            todoItem = new TodoItem();
            CreationResult<TodoItem> creationResult = await _navigationService
                .Navigate<CreateTodoItemViewModel, TodoItem, CreationResult<TodoItem>>(todoItem);
            
            if (creationResult != null && creationResult.IsSucceded)
            {
                TodoItems.Add(todoItem);
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
