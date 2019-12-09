using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.ApiModels.TodoItem;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class TodoItemListViewModel : BaseViewModel
    {
        private readonly ITodoItemService _todoItemService;

        public TodoItemListViewModel(IMvxNavigationService navigationService, ITodoItemService todoItemService,
            IStorageHelper storage)
            : base(navigationService, storage)
        {
            _todoItemService = todoItemService;

            ShowMenuCommand = new MvxAsyncCommand(async ()
                  => await _navigationService.Navigate<MenuViewModel>());
            AddTodoItemCommand = new MvxAsyncCommand(AddTodoItem);
            SelectTodoItemCommand = new MvxAsyncCommand<TodoItemGetListTodoItemApiModelItem>(SelectTodoItem);
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

        public IMvxAsyncCommand AddTodoItemCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        public IMvxCommand RefreshTodoItemsCommand { get; private set; }

        public IMvxCommand<TodoItemGetListTodoItemApiModelItem> SelectTodoItemCommand { get; private set; }

        public MvxNotifyTask LoadTodoItemsTask { get; private set; }

        private async Task SelectTodoItem(TodoItemGetListTodoItemApiModelItem selectedTodoItem)
        {
            BaseTodoItemResponse response = await _navigationService
                .Navigate<EditTodoItemViewModel, int, BaseTodoItemResponse>(selectedTodoItem.Id);

            await LoadTodoItems();
        }

        private async Task AddTodoItem()
        {
            ResponseCreateTodoItemApiModel response = await _navigationService
                .Navigate<CreateTodoItemViewModel, ResponseCreateTodoItemApiModel>();

            await LoadTodoItems();
        }

        private async Task LoadTodoItems()
        {
            TodoItems.Clear();

            GetListTodoItemApiModel usersTodoItems = await _todoItemService.GetUsersTodoItems();
            TodoItems.AddRange(usersTodoItems.TodoItems);
        }

        private void RefreshTodoItems()
        {
            LoadTodoItemsTask = MvxNotifyTask.Create(LoadTodoItems);
            RaisePropertyChanged(() => LoadTodoItemsTask);
        }
    }
}
