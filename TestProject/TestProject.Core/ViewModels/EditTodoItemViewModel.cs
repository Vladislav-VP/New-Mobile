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
        private string _description;
        private bool _isDone;
        private TodoItem _todoItem;

        public EditTodoItemViewModel(IMvxNavigationService navigationService) 
            : base(navigationService)
        {
            _todoItemRepository = new TodoItemRepository();
        }


        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private bool IsDone
        {
            get => _isDone;
            set
            {
                _isDone = value;
                RaisePropertyChanged(() => IsDone);
            }
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


        public override void Prepare(TodoItem parameter)
        {
            TodoItem = parameter;
        }
    }
}
