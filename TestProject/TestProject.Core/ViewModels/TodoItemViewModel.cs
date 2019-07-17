using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;

namespace TestProject.Core.ViewModels
{
    public abstract class TodoItemViewModel : BaseViewModel
    {
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

        public TodoItemViewModel(IMvxNavigationService navigationService)
            : base(navigationService) { }
    }
}
