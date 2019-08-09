using MvvmCross.Navigation;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Entities;

namespace TestProject.Core.ViewModels
{
    public abstract class TodoItemViewModel : BaseViewModel
    {
        protected string _name;
        protected string _description;
        protected bool _isDone;

        protected readonly IDialogsHelper _dialogsHelper;

        protected readonly IValidationHelper _validationHelper;

        protected readonly ITodoItemRepository _todoItemRepository;

        public TodoItemViewModel(IMvxNavigationService navigationService,  IValidationHelper validationHelper,
            ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : this(navigationService, null, validationHelper, todoItemRepository, dialogsHelper) { }

        public TodoItemViewModel(IMvxNavigationService navigationService, IStorageHelper<User> storage,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage)
        {
            _todoItemRepository = todoItemRepository;
            _validationHelper = validationHelper;

            _dialogsHelper = dialogsHelper;
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        public bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                RaisePropertyChanged(() => IsDone);
            }
        }
    }
}
