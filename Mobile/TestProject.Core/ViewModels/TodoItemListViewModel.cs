using System.Collections.Generic;
using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

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
            AddTodoItemCommand = new MvxAsyncCommand<TEntity>(AddTodoItem);
            SelectTodoItemCommand = new MvxAsyncCommand<TEntity>(SelectTodoItem);
            RefreshTodoItemsCommand = new MvxCommand(RefreshTodoItems);
        }
        
        private MvxObservableCollection<TEntity> _todoItems;
        public MvxObservableCollection<TEntity> TodoItems
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

            TodoItems = new MvxObservableCollection<TEntity>();
            LoadTodoItemsTask = MvxNotifyTask.Create(LoadTodoItems);
        }

        public IMvxAsyncCommand<TEntity> AddTodoItemCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        public IMvxCommand RefreshTodoItemsCommand { get; private set; }

        public IMvxCommand<TEntity> SelectTodoItemCommand { get; private set; }

        public MvxNotifyTask LoadTodoItemsTask { get; private set; }

        private async Task SelectTodoItem(TEntity selectedTodoItem)
        {
            ViewModelResult<TEntity> result = await _navigationService
                 .Navigate<EditTodoItemViewModel, TEntity, ViewModelResult<TEntity>>(selectedTodoItem);

            if (result is DeletionResult<TEntity> && result.IsSucceded)
            {
                TodoItems.Remove(selectedTodoItem);
                return;
            }

            if(result is UpdateResult<TEntity> && result.IsSucceded)
            {
                int index = TodoItems.IndexOf(selectedTodoItem);
                TodoItems.Insert(index, result.Entity);
                TodoItems.Remove(selectedTodoItem);
            }
        }

        private async Task AddTodoItem(TEntity todoItem)
        {
            ViewModelResult<TEntity> creationResult = await _navigationService
                .Navigate<CreateTodoItemViewModel, CreationResult<TEntity>>();
            
            if (creationResult != null && creationResult.IsSucceded)
            {
                TodoItems.Add(creationResult.Entity);
            }
        }

        private async Task LoadTodoItems()
        {
            TodoItems.Clear();

            IEnumerable<TEntity> retrievedTodoItems = await _todoItemService.Get();
            TodoItems.AddRange(retrievedTodoItems);
        }

        private void RefreshTodoItems()
        {
            LoadTodoItemsTask = MvxNotifyTask.Create(LoadTodoItems);
            RaisePropertyChanged(() => LoadTodoItemsTask);
        }
    }
}
