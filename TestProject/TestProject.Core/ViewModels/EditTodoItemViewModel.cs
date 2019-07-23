using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Entities;
using TestProject.Services;
using TestProject.Core.ViewModelResults;
using System.Threading.Tasks;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : TodoItemViewModel, IMvxViewModel<TodoItem>
    {
        private readonly ITodoItemRepository _todoItemRepository;

        private TodoItem _todoItem;

        public EditTodoItemViewModel(IMvxNavigationService navigationService) 
            : base(navigationService)
        {
            _todoItemRepository = new TodoItemRepository();
        }

        public TodoItem TodoItem
        {
            get => _todoItem;
            set
            {
                _todoItem = value;
                RaisePropertyChanged(() => TodoItem);
            }
        }

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
    }
}
