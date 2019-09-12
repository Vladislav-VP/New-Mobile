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

            // TODO : Uncomment line below after view defined
            //TodoItems = new MvxObservableCollection<TodoItem>(await _todoItemRepository.GetTodoItems(currentUser.Id));
        }

        public IMvxAsyncCommand<TodoItem> AddTodoItemCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuCommand { get; private set; }

        public IMvxCommand<TodoItem> SelectTodoItemCommand { get; private set; }

        private async Task SelectTodoItem(TodoItem selectedTodoItem)
        {
            DeletionResult<TodoItem> deletionResult = await _navigationService
                .Navigate<EditTodoItemViewModel, TodoItem, DeletionResult<TodoItem>>(selectedTodoItem);

            if (deletionResult != null && deletionResult.IsDeleted)
            {
                TodoItems.Remove(selectedTodoItem);
            }
        }

        private async Task AddTodoItem(TodoItem todoItem)
        {
            todoItem = new TodoItem();
            await _navigationService.Navigate<CreateTodoItemViewModel>();
            CreationResult<TodoItem> creationResult = await _navigationService
                .Navigate<CreateTodoItemViewModel, TodoItem, CreationResult<TodoItem>>(todoItem);


            if (creationResult != null && creationResult.IsCreated)
            {
                TodoItems.Add(todoItem);
            }
        }
    }
}
