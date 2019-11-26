using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.ApiModels.TodoItem;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class CreateTodoItemViewModel : TodoItemViewModel, IMvxViewModelResult<ResponseCreateTodoItemApiModel>
    {
        public CreateTodoItemViewModel(IMvxNavigationService navigationService, IStorageHelper storage, ICancelDialogService cancelDialogService, IUserService userService,
            IValidationHelper validationHelper,  IDialogsHelper dialogsHelper, ITodoItemService todoItemService)
            : base(navigationService, storage, cancelDialogService, validationHelper, dialogsHelper, todoItemService)
        {
            CreateTodoItemCommand = new MvxAsyncCommand(HandleEntity);
        }

        protected override bool IsStateChanged
        {
            get
            {
                return !string.IsNullOrWhiteSpace(Name)
                    || !string.IsNullOrWhiteSpace(Description)
                    || IsDone;
            }
        }

        public IMvxAsyncCommand CreateTodoItemCommand { get; private set; }

        protected override async Task HandleEntity()
        {
            int userId = await _storage.Get();
            var todoItem = new RequestCreateTodoItemApiModel
            {
                Name = Name,
                Description = Description,
                IsDone = IsDone,
                UserId = userId
            };

            ResponseCreateTodoItemApiModel response = await _todoItemService.CreateTodoItem(todoItem);
            if (response.IsSuccess)
            {
                await _navigationService.Close(this, result: response);
            }
        }
    }
}
