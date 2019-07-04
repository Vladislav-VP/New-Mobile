using System;
using System.Collections.Generic;
using System.Text;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using TestProject.Core.Models;

namespace TestProject.Core.ViewModels
{
    public class EditTodoItemViewModel : BaseTodoItemViewModel
    {
        public EditTodoItemViewModel(IMvxNavigationService navigationService) 
            : base(navigationService)
        {
        }
    }
}
