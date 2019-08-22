using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class TodoItemViewModel : BaseViewModel
    {
        protected readonly IDialogsHelper _dialogsHelper;

        protected readonly IValidationHelper _validationHelper;

        protected readonly ITodoItemRepository _todoItemRepository;

        public TodoItemViewModel(IMvxNavigationService navigationService,  IValidationHelper validationHelper,
            ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : this(navigationService, null, validationHelper, todoItemRepository, dialogsHelper) { }

        public TodoItemViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage)
        {
            _todoItemRepository = todoItemRepository;
            _validationHelper = validationHelper;

            _dialogsHelper = dialogsHelper;
        }

        protected string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        protected string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        protected bool _isDone;
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
