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
            IValidationResultHelper validationResultHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : this(navigationService, null, validationHelper, validationResultHelper, todoItemRepository, dialogsHelper) { }

        public TodoItemViewModel(IMvxNavigationService navigationService, IUserStorageHelper storage, IValidationHelper validationHelper,
            IValidationResultHelper validationResultHelper, ITodoItemRepository todoItemRepository, IDialogsHelper dialogsHelper)
            : base(navigationService, storage, dialogsHelper, validationHelper, validationResultHelper)
        {
            _todoItemRepository = todoItemRepository;
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

        protected override async Task<bool> TryValidateData()
        {
            var todoItem = new TodoItem { Name = Name, Description = Description, IsDone = IsDone };
            bool isTodoItemValid = _validationHelper.TryValidateObject<TodoItem>(todoItem);
            if (!isTodoItemValid)
            {
                _validationResultHelper.HandleValidationResult(todoItem);
                return false;
            }
            return true;
        }
    }
}
