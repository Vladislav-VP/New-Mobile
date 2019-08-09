using System.Threading.Tasks;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{

    public class TodoListItemViewModel : BaseViewModel
    {
        private readonly ITodoItemRepository _todoItemRepository;

        private MvxObservableCollection<TodoItem> _todoItems;

        public TodoListItemViewModel(IMvxNavigationService navigationService, 
            IStorageHelper<User> storage, ITodoItemRepository todoItemRepository)
            : base(navigationService, storage)
        {
            _todoItemRepository = todoItemRepository;

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
            
            User currentUser = await _storage.Retrieve();
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
