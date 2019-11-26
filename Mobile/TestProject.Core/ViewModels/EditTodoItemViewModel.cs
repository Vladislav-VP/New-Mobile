using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.ApiModels.TodoItem;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : TodoItemViewModel, IMvxViewModel<int, BaseTodoItemResponse>,
        IMvxViewModel<int, DeleteTodoItemApiModel>, IMvxViewModel<int, ResponseEditTodoItemApiModel>
    {
        private int _todoItemId;

        private GetTodoItemApiModel _oldTodoItem;

        public EditTodoItemViewModel(IMvxNavigationService navigationService,  IDialogsHelper dialogsHelper,
            ICancelDialogService cancelDialogService, IValidationHelper validationHelper, ITodoItemRepository todoItemRepository, ITodoItemService webService)
            : base(navigationService, validationHelper, cancelDialogService, todoItemRepository, dialogsHelper, webService)
        {
            UpdateTodoItemCommand = new MvxAsyncCommand(HandleEntity);
            DeleteTodoItemCommand = new MvxAsyncCommand(DeleteTodoItem);
        }

        protected override bool IsStateChanged 
        {
            get
            {
                return _oldTodoItem.Description != Description 
                    || _oldTodoItem.IsDone != IsDone;
            }
        }

        public IMvxAsyncCommand UpdateTodoItemCommand { get; private set; }

        public IMvxAsyncCommand DeleteTodoItemCommand { get; private set; }

        public void Prepare(int parameter)
        {
            _todoItemId = parameter;
        }

        public override async Task Initialize()
        {
            await base.Initialize();
            _oldTodoItem = await _todoItemService.GetTodoItem(_todoItemId);
            Name = _oldTodoItem.Name;
            Description = _oldTodoItem.Description;
            IsDone = _oldTodoItem.IsDone;
        }

        private async Task DeleteTodoItem()
        {
            bool isConfirmedToDelete = await _dialogsHelper.IsConfirmed(Strings.DeleteMessageDialog);

            if (!isConfirmedToDelete)
            {
                return;
            }
            DeleteTodoItemApiModel response = await _todoItemService.Delete<DeleteTodoItemApiModel>(_todoItemId);
            if (response.IsSuccess)
            {
                await _navigationService.Close(this, response);
            }
        }

        protected override async Task HandleEntity()
        {
            var todoItem = new RequestEditTodoItemApiModel
            {
                Id = _todoItemId,
                Name = Name,
                Description = Description,
                IsDone = IsDone
            };
            ResponseEditTodoItemApiModel response = await _todoItemService.EditTodoItem(todoItem);
            if (response.IsSuccess)
            {
                await _navigationService.Close(this, response);
            }
        }
    }
}
