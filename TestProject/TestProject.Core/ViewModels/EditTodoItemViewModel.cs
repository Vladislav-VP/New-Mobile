using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : BaseViewModel
    {
        public EditTodoItemViewModel(IMvxNavigationService navigationService) 
            : base(navigationService)
        {
        }
    }
}
