using System.Threading.Tasks;

using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class TodoItemViewModel : BaseEntityViewModel
    {
        protected readonly ITodoItemRepository _todoItemRepository;

        public TodoItemViewModel(IMvxNavigationService navigationService, IValidationHelper validationHelper,
            ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : this(navigationService, null, validationHelper, todoItemRepository, dialogsHelper) { }

        public TodoItemViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, dialogsHelper, validationHelper)
        {
            _todoItemRepository = todoItemRepository;
        }

        protected TodoItem _todoItem;
        public TodoItem TodoItem
        {
            get => _todoItem;
            set
            {
                _todoItem = value;
                RaisePropertyChanged(() => TodoItem);
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private bool _isDone;
        public bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                RaisePropertyChanged(() => IsDone);
            }
        }

        protected override async Task<bool> IsDataValid()
        {
            var todoItem = new TodoItem { Name = Name, Description = Description, IsDone = IsDone };
            bool isTodoItemValid = _validationHelper.IsObjectValid(todoItem);
            if (!isTodoItemValid)
            {
                return false;
            }

            return true;
        }
    }
}
