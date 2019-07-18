using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;

namespace TestProject.Core.ViewModels
{
    public abstract class TodoItemViewModel : BaseViewModel
    {
        protected string _name;
        protected string _description;
        protected bool _isDone;

        public TodoItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService) { }

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
