using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Services;
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

            TodoItems = new MvxObservableCollection<TodoItem>();
            LoadTodoItemsTask = MvxNotifyTask.Create(LoadTodoItems);
            await GetTodoItems();
        }

        public IMvxAsyncCommand<TodoItem> AddTodoItemCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        public IMvxCommand RefreshTodoItemsCommand { get; private set; }

        public IMvxCommand<TodoItem> SelectTodoItemCommand { get; private set; }

        public MvxNotifyTask LoadTodoItemsTask { get; private set; }

        private async Task SelectTodoItem(TodoItem selectedTodoItem)
        {
            ViewModelResult<TodoItem> result = await _navigationService
                 .Navigate<EditTodoItemViewModel, TodoItem, ViewModelResult<TodoItem>>(selectedTodoItem);

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
            TodoItems.Clear();

            User currentUser = await _storage.Get();
            IEnumerable<TodoItem> retrievedTodoItems = await _todoItemRepository.GetTodoItems(currentUser.Id);

            TodoItems.AddRange(retrievedTodoItems);
        }

        private void RefreshTodoItems()
        {
            LoadTodoItemsTask = MvxNotifyTask.Create(LoadTodoItems);
            RaisePropertyChanged(() => LoadTodoItemsTask);
        }










        // TODO: Refactor code below.















        bool initialized = false;   // была ли начальная инициализация
        private bool isBusy;    // идет ли загрузка с сервера

        WebService _webService = new WebService();

        public ICommand CreateFriendCommand { protected set; get; }
        public ICommand DeleteFriendCommand { protected set; get; }
        public ICommand SaveFriendCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }


        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }
        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        //public ApplicationViewModel()
        //{
        //    IsBusy = false;
        //    CreateFriendCommand = new Command(CreateFriend);
        //    DeleteFriendCommand = new Command(DeleteFriend);
        //    SaveFriendCommand = new Command(SaveFriend);
        //    BackCommand = new Command(Back);
        //}


        private TodoItem _todoItem;
        public TodoItem TodoItem
        {
            get { return _todoItem; }
            set
            {

            }
        }
        protected void OnPropertyChanged(string propName)
        {
            

        }

        private async void CreateFriend()
        {

        }
        private void Back()
        {

        }




        public async Task GetTodoItems()
        {
            if (initialized == true) return;
            IsBusy = true;
            IEnumerable<TodoItem> mockedTodoItems = await _webService.Get();

            // очищаем список
            //Friends.Clear();

            // добавляем загруженные данные
            IsBusy = false;
            TodoItems.AddRange(mockedTodoItems);
            initialized = true;
        }

    }
}
