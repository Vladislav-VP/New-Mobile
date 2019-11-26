using MvvmCross.Navigation;

using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public abstract class TodoItemViewModel : BaseEntityViewModel
    {

        protected readonly IValidationHelper _validationHelper;

        protected readonly ITodoItemService _todoItemService;

        public TodoItemViewModel(IMvxNavigationService navigationService, IValidationHelper validationHelper,
            ICancelDialogService cancelDialogService, IDialogsHelper dialogsHelper, ITodoItemService todoItemService)
            : this(navigationService, null, cancelDialogService, validationHelper, dialogsHelper, todoItemService) { }

        public TodoItemViewModel(IMvxNavigationService navigationService, IStorageHelper storage, ICancelDialogService cancelDialogService,
            IValidationHelper validationHelper, IDialogsHelper dialogsHelper, ITodoItemService todoItemService)
            : base(navigationService, storage, dialogsHelper, cancelDialogService)
        {
            _validationHelper = validationHelper;
            _todoItemService = todoItemService;
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
    }
}
