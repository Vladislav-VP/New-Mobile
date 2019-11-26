using System.Collections.Generic;
using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.ApiModels.TodoItem;
using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class TodoItemListViewModel : BaseViewModel
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly ITodoItemService _todoItemService;

        public TodoItemListViewModel(IMvxNavigationService navigationService, ITodoItemService todoItemService,
            IUserStorageHelper storage, ITodoItemRepository todoItemRepository)
            : base(navigationService, storage)
        {
            _todoItemRepository = todoItemRepository;
            _todoItemService = todoItemService;

            ShowMenuCommand = new MvxAsyncCommand(async ()
                  => await _navigationService.Navigate<MenuViewModel>());
            AddTodoItemCommand = new MvxAsyncCommand<TodoItem>(AddTodoItem);
            SelectTodoItemCommand = new MvxAsyncCommand<TodoItem>(SelectTodoItem);
            RefreshTodoItemsCommand = new MvxCommand(RefreshTodoItems);
        }
        
        private MvxObservableCollection<TodoItemGetListTodoItemApiModelItem> _todoItems;
        public MvxObservableCollection<TodoItemGetListTodoItemApiModelItem> TodoItems
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

            TodoItems = new MvxObservableCollection<TodoItemGetListTodoItemApiModelItem>();
            LoadTodoItemsTask = MvxNotifyTask.Create(LoadTodoItems);
        }

        public IMvxAsyncCommand<TodoItem> AddTodoItemCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        public IMvxCommand RefreshTodoItemsCommand { get; private set; }

        public IMvxCommand<TodoItem> SelectTodoItemCommand { get; private set; }

        public MvxNotifyTask LoadTodoItemsTask { get; private set; }

        private async Task SelectTodoItem(TodoItem selectedTodoItem)
        {
            //ViewModelResult<TodoItem> result = await _navigationService
            //     .Navigate<EditTodoItemViewModel, TodoItem, ViewModelResult<TodoItem>>(selectedTodoItem);

            //if (result is DeletionResult<TodoItem> && result.IsSucceded)
            //{
            //    TodoItems.Remove(selectedTodoItem);
            //    return;
            //}

            //if(result is UpdateResult<TodoItem> && result.IsSucceded)
            //{
            //    int index = TodoItems.IndexOf(selectedTodoItem);
            //    TodoItems.Insert(index, result.Entity);
            //    TodoItems.Remove(selectedTodoItem);
            //}
        }

        private async Task AddTodoItem(TodoItem todoItem)
        {
            //ViewModelResult<TodoItem> creationResult = await _navigationService
            //    .Navigate<CreateTodoItemViewModel, CreationResult<TodoItem>>();
            
            //if (creationResult != null && creationResult.IsSucceded)
            //{
            //    TodoItems.Add(creationResult.Entity);
            //}
        }

        private async Task LoadTodoItems()
        {
            TodoItems.Clear();

            int userId = await _storage.Get();
            GetListTodoItemApiModel usersTodoItems = await _todoItemService.GetUsersTodoItems(userId);
            TodoItems.AddRange(usersTodoItems.TodoItems);
        }

        private void RefreshTodoItems()
        {
            LoadTodoItemsTask = MvxNotifyTask.Create(LoadTodoItems);
            RaisePropertyChanged(() => LoadTodoItemsTask);
        }
    }
}
