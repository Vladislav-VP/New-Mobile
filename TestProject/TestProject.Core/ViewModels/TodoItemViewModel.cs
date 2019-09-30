using System.Threading.Tasks;
using MvvmCross.Navigation;

using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class TodoItemViewModel : BaseEntityViewModel
    {
        protected readonly ITodoItemRepository _todoItemRepository;

        protected readonly IValidationHelper _validationHelper;

        public TodoItemViewModel(IMvxNavigationService navigationService, IValidationHelper validationHelper,
            ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : this(navigationService, null, validationHelper, todoItemRepository, dialogsHelper) { }

        public TodoItemViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, dialogsHelper)
        {
            _validationHelper = validationHelper;
            _todoItemRepository = todoItemRepository;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
                IsStateChanged = true;
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
                IsStateChanged = true;
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
                IsStateChanged = true;
            }
        }

        public override Task Initialize()
        {
            IsStateChanged = false;

            return base.Initialize();
        }
    }
}
