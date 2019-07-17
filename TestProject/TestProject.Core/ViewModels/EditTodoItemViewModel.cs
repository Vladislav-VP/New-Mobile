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

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : BaseViewModel<TodoItem, DestructionResult<TodoItem>>
    {
        private readonly ITodoItemRepository _todoItemRepository;

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

        protected bool _isDone;
        private bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                RaisePropertyChanged(() => IsDone);
            }
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


        public EditTodoItemViewModel(IMvxNavigationService navigationService) 
            : base(navigationService)
        {
            _todoItemRepository = new TodoItemRepository();
        }

        public override void Prepare(TodoItem parameter)
        {
            TodoItem = parameter;
        }
    }
}
