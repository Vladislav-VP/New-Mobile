using System.Threading.Tasks;

using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

using TestProject.Core.ViewModelResults;
using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Repositories.Interfaces;


namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : TodoItemViewModel, IMvxViewModel<TodoItem>
    {        
        public EditTodoItemViewModel(IMvxNavigationService navigationService,  IDialogsHelper dialogsHelper,
            IValidationHelper validationHelper, IValidationResultHelper validationResultHelper, ITodoItemRepository todoItemRepository)
            : base(navigationService, validationHelper, validationResultHelper, todoItemRepository, dialogsHelper)
        {
            UpdateTodoItemCommand = new MvxAsyncCommand(UpdateTodoItem);
            DeleteTodoItemCommand = new MvxAsyncCommand(DeleteTodoItem);
        }

        private TodoItem _todoItem;
        public TodoItem TodoItem
        {
            get => _todoItem;
            set
            {
                _todoItem = value;
                RaisePropertyChanged(() => TodoItem);
            }
        }

        public IMvxAsyncCommand UpdateTodoItemCommand { get; private set; }

        public IMvxAsyncCommand DeleteTodoItemCommand { get; private set; }
        
        public void Prepare(TodoItem parameter)
        {
            TodoItem = parameter;
        }

        public async override Task Initialize()
        {
            await base.Initialize();

            _name = TodoItem.Name;
            _description = TodoItem.Description;
            _isDone = TodoItem.IsDone;
        }

        protected override async Task GoBack()
        {
            await base.GoBack();

            DialogResult result = await _navigationService.Navigate<CancelDialogViewModel, DialogResult>();
            if (result == DialogResult.Yes)
            {
                await UpdateTodoItem();
                return;
            }
        }

        private async Task UpdateTodoItem()
        {
            ChangeTodoItem();
            bool isTodoItemValid = await TryValidateData();
            if (!isTodoItemValid)
            {
                return;
            }

            await _todoItemRepository.Update(TodoItem);
            await _navigationService.Navigate<TodoListItemViewModel>();
        }
        private void ChangeTodoItem()
        {
            TodoItem.Name = Name;
            TodoItem.Description = Description;
            TodoItem.IsDone = IsDone;
        }

        private async Task DeleteTodoItem()
        {
            bool isToDelete = await _dialogsHelper.TryGetConfirmation(Strings.DeleteMessageDialog);

            if (!isToDelete)
            {
                return;
            }

            await _todoItemRepository.Delete(TodoItem);

            await _navigationService.Navigate<TodoListItemViewModel>();
        }
    }
}
