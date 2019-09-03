using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{

    public class TodoListItemViewModel : BaseViewModel
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoListItemViewModel(IMvxNavigationService navigationService,
            IUserStorageHelper storage, ITodoItemRepository todoItemRepository)
            : base(navigationService, storage)
        {
            _todoItemRepository = todoItemRepository;

            ShowMenuViewModelCommand = new MvxAsyncCommand(async ()
                  => await _navigationService.Navigate<MenuViewModel>());
            ShowCreateTodoItemViewModelCommand = new MvxAsyncCommand(async ()
                => await _navigationService.Navigate<CreateTodoItemViewModel>());
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
            if (currentUser == null)
            {
                await _navigationService.Navigate<LoginViewModel>();
                return;
            }
            TodoItems = new MvxObservableCollection<TodoItem>(await _todoItemRepository.GetTodoItems(currentUser.Id));
        }

        public IMvxAsyncCommand ShowCreateTodoItemViewModelCommand { get; private set; }

        public IMvxAsyncCommand ShowMenuViewModelCommand { get; private set; }

        public IMvxCommand<TodoItem> SelectTodoItemCommand { get; private set; }

        private async Task SelectTodoItem(TodoItem selectedTodoItem)
        {
            await _navigationService.Navigate<TodoItem>(typeof(EditTodoItemViewModel), selectedTodoItem);
        }
    }
}
