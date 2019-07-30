using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using TestProject.Services.Helpers;
using TestProject.Services.Repositories.Interfaces;
using TestProject.Services.Repositories;
using TestProject.Services.Helpers.Interfaces;
using Acr.UserDialogs;

namespace TestProject.Core.ViewModels
{
    public abstract class TodoItemViewModel : BaseViewModel
    {
        protected string _name;
        protected string _description;
        protected bool _isDone;

        protected readonly IDialogsHelper _dialogsHelper;

        protected readonly IUserDialogs _userDialogs;

        protected readonly ITodoItemRepository _todoItemRepository;

        public TodoItemViewModel(IMvxNavigationService navigationService, IUserDialogs userDialogs)
            : base(navigationService)
        {
            _todoItemRepository = new TodoItemRepository();

            _userDialogs = userDialogs;

            _dialogsHelper = new UserDialogsHelper();
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
