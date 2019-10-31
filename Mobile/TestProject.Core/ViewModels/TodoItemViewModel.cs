using System.Threading.Tasks;

using MvvmCross.Navigation;

using TestProject.Entities;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class TodoItemViewModel : BaseEntityViewModel
    {
        private TodoItem _unmodifiedTodoItem;

        protected readonly ITodoItemRepository _todoItemRepository;

        protected readonly IValidationHelper _validationHelper;

        protected readonly ITodoItemService _todoItemService;

        public TodoItemViewModel(IMvxNavigationService navigationService, IValidationHelper validationHelper,
            ICancelDialogService cancelDialogService, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper, ITodoItemService todoItemService)
            : this(navigationService, null, cancelDialogService, validationHelper, todoItemRepository, dialogsHelper, todoItemService) { }

        public TodoItemViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, ICancelDialogService cancelDialogService,
            IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper, ITodoItemService todoItemService)
            : base(navigationService, storage, dialogsHelper, cancelDialogService)
        {
            _validationHelper = validationHelper;
            _todoItemRepository = todoItemRepository;
            _todoItemService = todoItemService;
        }

        protected override bool IsStateChanged
        {
            get
            {
                return Name != _unmodifiedTodoItem.Name
                    || Description != _unmodifiedTodoItem.Description
                    || IsDone != _unmodifiedTodoItem.IsDone;
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

        public override Task Initialize()
        {
            _unmodifiedTodoItem = new TodoItem
            {
                Name = Name,
                Description = Description,
                IsDone = IsDone
            };

            return base.Initialize();
        }
    }
}
